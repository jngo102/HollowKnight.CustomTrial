using CustomTrial.Classes;
using HutongGames.PlayMaker.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vasi;

namespace CustomTrial
{
    public class ColosseumManager : MonoBehaviour
    {
        private List<Vector2> _platPos = new List<Vector2>();
        private List<GameObject> _platforms = new List<GameObject>();

        private Dictionary<string, GameObject> _queuedEnemies = new Dictionary<string, GameObject>();
        
        private PlayMakerFSM _battleCtrl;
        private PlayMakerFSM _manager;
        private PlayMakerFSM _crowdAudioCtrl;
        private PlayMakerFSM _musicCtrl;

        private GameObject _groundSpikes;
        private GameObject _respawnPlat;

        private void Awake()
        {
            _battleCtrl = gameObject.LocateMyFSM("Battle Control");
            _manager = gameObject.LocateMyFSM("Manager");
            _crowdAudioCtrl = gameObject.transform.Find("Crowd Audio").gameObject.LocateMyFSM("Control");
            _musicCtrl = gameObject.transform.Find("Music Control").gameObject.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            CustomTrial.GameObjects["Large Cage"] = Instantiate(gameObject.transform.Find("Waves/Wave 4/Colosseum Cage Large").gameObject);
            CustomTrial.GameObjects["Small Cage"] = Instantiate(gameObject.transform.Find("Waves/Wave 4/Colosseum Cage Small").gameObject);
            CustomTrial.GameObjects["Platform"] = Instantiate(gameObject.transform.Find("Waves/Arena 1/Colosseum Platform").gameObject);

            GameObject waves = gameObject.transform.Find("Waves").gameObject;
            _respawnPlat = waves.transform.Find("Respawn Plat").gameObject;
            _respawnPlat.SetActive(true);
            GameObject respawnPlatform = _respawnPlat.transform.Find("Respawn Platform").gameObject;
            respawnPlatform.LocateMyFSM("Control").SetState("Init");
            foreach (Transform childTransform in waves.transform)
            {
                GameObject child = childTransform.gameObject;
                if (child.name != "Respawn Plat")
                {
                    Destroy(child);
                }
            }
            
            Destroy(_battleCtrl);

            foreach (GameObject go in FindObjectsOfType<GameObject>())
            {
                if (go.layer == 11)
                {
                    Destroy(go);
                }
            }

            for (int i = 0; i < CustomTrial.BattleControl.Waves.Count; i++)
            {
                Wave wave = CustomTrial.BattleControl.Waves[i];
                for (int j = 0; j < wave.Enemies.Count; j++)
                {
                    Enemy enemy = wave.Enemies[j];

                    GameObject spawnedEnemy = SpawnEnemy(enemy.Name, enemy.SpawnPosition);
                    spawnedEnemy.GetComponent<HealthManager>().hp = enemy.Health;
                    _queuedEnemies.Add($"{i}:{enemy.Name}:{j}", spawnedEnemy);
                }
            }

            _groundSpikes = gameObject.transform.Find("Ground Spikes").gameObject;
            GameObject spike = _groundSpikes.transform.Find("Colosseum Spike (19)").gameObject;
            PlayMakerFSM damagesEnemy = spike.LocateMyFSM("damages_enemy");
            damagesEnemy.Fsm.GetFsmInt("damageDealt").Value = 0;
            
            yield return new WaitWhile(() => _manager.ActiveStateName != "Waves Start");
            
            StartCoroutine(StartWaves());
        }

        public static int EnemyCount;
        private IEnumerator StartWaves()
        {    
            Log("Start Waves");
            for (int i = 0; i < CustomTrial.BattleControl.Waves.Count; i++)
            {
                Wave wave = CustomTrial.BattleControl.Waves[i];
                foreach (GameObject crowd in CustomTrial.Crowds)
                {
                    crowd.LocateMyFSM("Control").SendEvent("CROWD IDLE");
                }
                _crowdAudioCtrl.SendEvent("CROWD IDLE");

                _musicCtrl.SendEvent("MUSIC " + wave.MusicLevel);

                EnemyCount = 0;
                
                PlayMakerFSM spawn;

                yield return new WaitForSeconds(wave.Cooldown);

                List<Vector2> newPlats = new List<Vector2>();
                foreach (Vector2 platSpawn in wave.PlatformSpawn)
                {
                    newPlats.Add(platSpawn);
                    if (!_platPos.Contains(platSpawn))
                    {
                        GameObject platform = Instantiate(CustomTrial.GameObjects["Platform"], platSpawn, Quaternion.identity);
                        platform.SetActive(true);

                        PlayMakerFSM platCtrl = platform.LocateMyFSM("Control");
                        platCtrl.SetState("Init");

                        yield return new WaitWhile(() => platCtrl.ActiveStateName != "Retracted");
                        
                        platCtrl.SendEvent("PLAT EXPAND");
                        
                        _platPos.Add(platSpawn);
                        _platforms.Add(platform);
                    }
                }

                List<Vector2> platPosToRemove = new List<Vector2>();

                foreach (Vector2 platPos in _platPos)
                {
                    if (!newPlats.Contains(platPos))
                    {
                        platPosToRemove.Add(platPos);
                    }
                }

                foreach (Vector2 platPos in platPosToRemove)
                {
                    RetractPlatform(platPos);
                }

                GameObject wallC = gameObject.transform.Find("Walls/Colosseum Wall C").gameObject;
                GameObject wallL = gameObject.transform.Find("Walls/Colosseum Wall L").gameObject;
                GameObject wallR = gameObject.transform.Find("Walls/Colosseum Wall R").gameObject;

                PlayMakerFSM wallCCtrl = wallC.LocateMyFSM("Control");
                PlayMakerFSM wallLCtrl = wallL.LocateMyFSM("Control");
                PlayMakerFSM wallRCtrl = wallR.LocateMyFSM("Control");
                if (wave.WallCDistance >= 0.0f)
                {
                    wallCCtrl.Fsm.GetFsmFloat("Distance").Value = wave.WallCDistance;
                    wallCCtrl.SendEvent("MOVE");
                }

                if (wave.WallLDistance >= 0.0f)
                {
                    wallLCtrl.Fsm.GetFsmFloat("Distance").Value = wave.WallLDistance;
                    wallLCtrl.SendEvent("MOVE");
                }

                if (wave.WallRDistance >= 0.0f)
                {
                    wallRCtrl.Fsm.GetFsmFloat("Distance").Value = wave.WallRDistance;
                    wallRCtrl.SendEvent("MOVE");
                }

                yield return new WaitUntil(() => wallCCtrl.ActiveStateName == "Idle" && wallLCtrl.ActiveStateName == "Idle" && wallRCtrl.ActiveStateName == "Idle");

                float wallCY = wallC.transform.Find("Wall").transform.position.y;
                float wallLX = wallL.transform.Find("Wall").transform.position.x;
                float wallRX = wallR.transform.Find("Wall").transform.position.x;
                float xOffset = 6;
                ArenaInfo.TopY = wallCY;
                ArenaInfo.LeftX = wallLX;
                ArenaInfo.RightX = wallRX;
                ArenaInfo.CenterX = wallLX + (wallRX - wallLX) / 2.0f + xOffset;
                ArenaInfo.CenterY = ArenaInfo.BottomY + (wallCY - ArenaInfo.BottomY) / 2.0f;
                Vector2 respawnPos = new Vector2(ArenaInfo.CenterX, ArenaInfo.CenterY);
                _respawnPlat.transform.position = respawnPos;
                HeroController.instance.SetHazardRespawn(respawnPos + Vector2.up * 0.2f + Vector2.left * xOffset, true);

                ArenaInfo.BottomY = ArenaInfo.DefaultBottomY;
                foreach (Transform spikeTransform in _groundSpikes.transform)
                {
                    GameObject spike = spikeTransform.gameObject;
                    PlayMakerFSM control = spike.LocateMyFSM("Control");
                    if (wave.Spikes)
                    {
                        ArenaInfo.BottomY = ArenaInfo.DefaultBottomY + 2;
                        control.SendEvent("EXPAND");
                    }
                    else
                    {
                        control.SendEvent("RETRACT");
                    }
                }

                for (int j = 0; j < wave.Enemies.Count; j++)
                {
                    int waveNum = i;
                    int enemyNum = j;
                    Enemy enemy = wave.Enemies[enemyNum];

                    GameObject largeCage = Instantiate(CustomTrial.GameObjects["Large Cage"], enemy.SpawnPosition, Quaternion.identity);
                    largeCage.SetActive(true);
                    spawn = largeCage.LocateMyFSM("Spawn");

                    spawn.GetState("Spawn").RemoveAction<ActivateGameObject>();
                    spawn.GetState("Spawn").InsertMethod(1, () => _queuedEnemies[$"{waveNum}:{enemy.Name}:{enemyNum}"].SetActive(true));
                    spawn.SetState("Init");
                    spawn.SendEvent("SPAWN");

                    EnemyCount++;

                    yield return new WaitForSeconds(wave.DelayBetweenSpawns);
                }

                //yield return new WaitWhile(() => spawn.ActiveStateName != "End" && spawn != null);
                yield return new WaitWhile(() => EnemyCount > 0);

                if (wave.CrowdAction == "Cheer")
                {
                    foreach (GameObject crowd in CustomTrial.Crowds)
                    {
                        crowd.LocateMyFSM("Control").SendEvent("CROWD CHEER");
                    }
                    _crowdAudioCtrl.SendEvent("CROWD SHORT CHEER");
                }
                else if (wave.CrowdAction == "Gasp")
                {
                    foreach (GameObject crowd in CustomTrial.Crowds)
                    {
                        crowd.LocateMyFSM("Control").SendEvent("CROWD GASP");
                    }
                    _crowdAudioCtrl.SendEvent("CROWD GASP");
                }
                else if (wave.CrowdAction == "Laugh")
                {
                    foreach (GameObject crowd in CustomTrial.Crowds)
                    {
                        crowd.LocateMyFSM("Control").SendEvent("CROWD LAUGH");
                    }
                    _crowdAudioCtrl.SendEvent("CROWD LAUGH");
                }
            }

            Log("Battle Over!");
            _manager.SendEvent("WAVES COMPLETED");
        }
        
        private GameObject SpawnEnemy(string enemyName, Vector2 spawnPoint)
        {
            GameObject enemy = Instantiate(CustomTrial.GameObjects[enemyName], spawnPoint, Quaternion.identity);
            enemy.SetActive(false);

            enemy.AddComponent<EnemyTracker>();

            var hm = enemy.GetComponent<HealthManager>();
            hm.SetGeoSmall(0);
            hm.SetGeoMedium(0);
            hm.SetGeoLarge(0);

            return enemy;
        }

        private void RetractPlatform(Vector2 platPos)
        {
            int index = _platPos.IndexOf(platPos);
            GameObject platform = _platforms[index];
            PlayMakerFSM platCtrl = platform.LocateMyFSM("Control");
            platCtrl.SendEvent("SLOW RETRACT");
            _platforms.RemoveAt(index);
            _platPos.Remove(platPos);
        }
        
        private void Log(object message) => Modding.Logger.Log("[Colosseum Manager] " + message);
    }
}