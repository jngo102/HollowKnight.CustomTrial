using System.Collections;
using System.Collections.Generic;
using CustomTrial.Classes;
using CustomTrial.Utilities;
using HutongGames.PlayMaker.Actions;
using ModCommon;
using UnityEngine;

namespace CustomTrial
{
    public class ColosseumManager : MonoBehaviour
    {
        private List<Wave> _waves = new List<Wave>();
        private List<Vector2> _platPos = new List<Vector2>();
        private List<GameObject> _platforms = new List<GameObject>();
        
        private PlayMakerFSM _battleCtrl;
        private PlayMakerFSM _manager;

        private GameObject _groundSpikes;
        private GameObject _respawnPlat;
        
        private void Awake()
        {
            _battleCtrl = gameObject.LocateMyFSM("Battle Control");
            _manager = gameObject.LocateMyFSM("Manager");
        }

        private IEnumerator Start()
        {
            GameObject waves = gameObject.FindGameObjectInChildren("Waves");
            _respawnPlat = waves.FindGameObjectInChildren("Respawn Plat");
            _respawnPlat.SetActive(true);
            GameObject respawnPlatform = _respawnPlat.FindGameObjectInChildren("Respawn Platform");
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

            _groundSpikes = gameObject.FindGameObjectInChildren("Ground Spikes");
            GameObject spike = _groundSpikes.FindGameObjectInChildren("Colosseum Spike (19)");
            PlayMakerFSM damagesEnemy = spike.LocateMyFSM("damages_enemy");
            damagesEnemy.Fsm.GetFsmInt("damageDealt").Value = 0;
            
            yield return new WaitWhile(() => _manager.ActiveStateName != "Waves Start");
            
            StartCoroutine(StartWaves());
        }

        public static int EnemyCount;
        private IEnumerator StartWaves()
        {    
            Log("Start Waves");
            foreach (Wave wave in CustomTrial.battleControl.Waves)
            {
                EnemyCount = 0;
                
                PlayMakerFSM spawn = null;

                yield return new WaitForSeconds(wave.Cooldown);

                List<Vector2> newPlats = new List<Vector2>();
                    
                Log("Platform Spawn");
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
                
                Log("Finding Platforms to Remove");
                foreach (Vector2 platPos in _platPos)
                {
                    if (!newPlats.Contains(platPos))
                    {
                        platPosToRemove.Add(platPos);
                    }
                }

                Log("Removing Platforms");
                foreach (Vector2 platPos in platPosToRemove)
                {
                    RetractPlatform(platPos);
                }

                Log("Finding Walls");
                GameObject wallC = gameObject.FindGameObjectInChildren("Colosseum Wall C");
                GameObject wallL = gameObject.FindGameObjectInChildren("Colosseum Wall L");
                GameObject wallR = gameObject.FindGameObjectInChildren("Colosseum Wall R");

                Log("Finding Wall FSMs");
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

                Log("WaitUntils");
                yield return new WaitUntil(() => wallCCtrl.ActiveStateName == "Idle" && wallLCtrl.ActiveStateName == "Idle" && wallRCtrl.ActiveStateName == "Idle");

                Log("Setting Respawn");
                
                float wallCY = wallC.FindGameObjectInChildren("Wall").transform.position.y;
                float wallLX = wallL.FindGameObjectInChildren("Wall").transform.position.x;
                float wallRX = wallR.FindGameObjectInChildren("Wall").transform.position.x;
                float xOffset = 6;
                ArenaInfo.TopY = wallCY;
                ArenaInfo.LeftX = wallLX;
                ArenaInfo.RightX = wallRX;
                ArenaInfo.CenterX = wallLX + (wallRX - wallLX) / 2.0f + xOffset;
                ArenaInfo.CenterY = ArenaInfo.BottomY + (wallCY - ArenaInfo.BottomY) / 2.0f;
                Log("Walls: " + ArenaInfo.LeftX + " " + ArenaInfo.RightX + " " + ArenaInfo.TopY + " " + ArenaInfo.BottomY + " " + ArenaInfo.CenterX + " " + ArenaInfo.CenterY);
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
                
                for (int i = 0; i < wave.Enemies.Count; i++)
                {
                    string enemyName = wave.Enemies[i];
                    Vector2 enemySpawn = wave.EnemySpawn[i];
                
                    GameObject largeCage = Instantiate(CustomTrial.GameObjects["Large Cage"], enemySpawn, Quaternion.identity);
                    largeCage.SetActive(true);
                    spawn = largeCage.LocateMyFSM("Spawn");

                    spawn.RemoveAction<ActivateGameObject>("Spawn");
                    spawn.InsertMethod("Spawn", 1, () => SpawnEnemy(enemyName, enemySpawn));
                    spawn.SetState("Init");
                    spawn.SendEvent("SPAWN");

                    EnemyCount++;
                }

                yield return new WaitWhile(() =>
                {
                    return spawn.ActiveStateName != "End";
                });

                yield return new WaitWhile(() => EnemyCount > 0);
            }

            Log("Battle Over!");
            
            yield return null;
        }
        
        private void SpawnEnemy(string enemyName, Vector2 spawnPoint)
        {
            GameObject enemy = Instantiate(CustomTrial.GameObjects[enemyName], spawnPoint, Quaternion.identity);
            enemy.SetActive(true);

            enemy.AddComponent<EnemyTracker>();

            var hm = enemy.GetComponent<HealthManager>();
            hm.SetGeoSmall(0);
            hm.SetGeoMedium(0);
            hm.SetGeoLarge(0);
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