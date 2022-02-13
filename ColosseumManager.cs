using CustomTrial.Classes;
using HutongGames.PlayMaker.Actions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vasi;


namespace CustomTrial
{
    internal class ColosseumManager : MonoBehaviour
    {
        private List<Vector2> _platPos = new();
        private List<GameObject> _platforms = new();

        private Dictionary<string, GameObject> _queuedEnemies = new();

        private GameObject _wallC;
        private GameObject _wallL;
        private GameObject _wallR;
        private PlayMakerFSM _wallCCtrl;
        private PlayMakerFSM _wallLCtrl;
        private PlayMakerFSM _wallRCtrl;

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
            _crowdAudioCtrl = transform.Find("Crowd Audio").gameObject.LocateMyFSM("Control");
            _musicCtrl = transform.Find("Music Control").gameObject.LocateMyFSM("Control");

            Transform walls = transform.Find("Walls");
            _wallC = walls.Find("Colosseum Wall C").gameObject;
            _wallL = walls.Find("Colosseum Wall L").gameObject;
            _wallR = walls.Find("Colosseum Wall R").gameObject;

            _wallCCtrl = _wallC.LocateMyFSM("Control");
            _wallLCtrl = _wallL.LocateMyFSM("Control");
            _wallRCtrl = _wallR.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            GameObject waves = gameObject.transform.Find("Waves").gameObject;

            // Environment
            CustomTrial.GameObjects["largecage"] = Instantiate(waves.transform.Find("Wave 4/Colosseum Cage Large").gameObject);
            CustomTrial.GameObjects["smallcage"] = Instantiate(waves.transform.Find("Wave 4/Colosseum Cage Small").gameObject);
            CustomTrial.GameObjects["platform"] = Instantiate(waves.transform.Find("Arena 1/Colosseum Platform").gameObject);

            // Enemies
            CustomTrial.GameObjects["armoredsquit"] = waves.transform.Find("Wave 12/Colosseum Cage Small (4)").gameObject.LocateMyFSM("Spawn").Fsm.GetFsmGameObject("Enemy Type").Value;
            CustomTrial.GameObjects["battleobble"] = waves.transform.Find("Wave 37/Colosseum Cage Small (3)").gameObject.LocateMyFSM("Spawn").Fsm.GetFsmGameObject("Enemy Type").Value;
            CustomTrial.GameObjects["belfly"] = waves.transform.Find("Wave 8/Colosseum Cage Small (4)").gameObject.LocateMyFSM("Spawn").Fsm.GetFsmGameObject("Enemy Type").Value;
            CustomTrial.GameObjects["deathloodle"] = waves.transform.Find("Wave 10/Colosseum Cage Small (5)").gameObject.LocateMyFSM("Spawn").Fsm.GetFsmGameObject("Enemy Type").Value;
            CustomTrial.GameObjects["furiousvengefly"] = waves.transform.Find("Wave 7/Colosseum Cage Small (1)").gameObject.LocateMyFSM("Spawn").Fsm.GetFsmGameObject("Enemy Type").Value;
            CustomTrial.GameObjects["heavyfool"] = waves.transform.Find("Wave 1/Colosseum Cage Large").gameObject.LocateMyFSM("Spawn").Fsm.GetFsmGameObject("Corpse to Instantiate").Value;
            CustomTrial.GameObjects["lancer"] = waves.transform.Find("Lobster Lancer/Entry Object/Lancer").gameObject;
            CustomTrial.GameObjects["lessermawlek"] = waves.transform.Find("Wave 31/Colosseum Cage Large (4)").gameObject.LocateMyFSM("Spawn").Fsm.GetFsmGameObject("Corpse to Instantiate").Value;
            CustomTrial.GameObjects["lobster"] = waves.transform.Find("Lobster Lancer/Entry Object/Lobster").gameObject;
            CustomTrial.GameObjects["mantispetra"] = waves.transform.Find("Wave 45/Colosseum Cage Small 1").gameObject.LocateMyFSM("Spawn").Fsm.GetFsmGameObject("Enemy Type").Value;
            CustomTrial.GameObjects["mantistraitor"] = waves.transform.Find("Wave 20/Colosseum Cage Large (5)").gameObject.LocateMyFSM("Spawn").Fsm.GetFsmGameObject("Corpse to Instantiate").Value;
            CustomTrial.GameObjects["primalaspid"] = waves.transform.Find("Wave 17/Colosseum Cage Small (2)").gameObject.LocateMyFSM("Spawn").Fsm.GetFsmGameObject("Enemy Type").Value;
            CustomTrial.GameObjects["sharpbaldur"] = waves.transform.Find("Wave 4/Colosseum Cage Small").gameObject.LocateMyFSM("Spawn").Fsm.GetFsmGameObject("Enemy Type").Value;
            CustomTrial.GameObjects["sturdyfool"] = waves.transform.Find("Wave 50/Colosseum Cage Large").gameObject.LocateMyFSM("Spawn").Fsm.GetFsmGameObject("Enemy").Value;
            CustomTrial.GameObjects["shieldedfool"] = waves.transform.Find("Wave 3/Colosseum Cage Large").gameObject.LocateMyFSM("Spawn").Fsm.GetFsmGameObject("Corpse to Instantiate").Value;
            CustomTrial.GameObjects["soultwister"] = waves.transform.Find("Wave 22/Mage").gameObject;
            CustomTrial.GameObjects["volttwister"] = waves.transform.Find("Wave 25/Electric Mage New").gameObject;
            CustomTrial.GameObjects["wingedfool"] = waves.transform.Find("Wave 6/Colosseum Cage Large").gameObject.LocateMyFSM("Spawn").Fsm.GetFsmGameObject("Corpse to Instantiate").Value;

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

            gameObject.LocateMyFSM("Geo Pool").Fsm.GetFsmInt("Starting Pool").Value =
                CustomTrial.GlobalSettings.GeoReward;

            foreach (GameObject go in FindObjectsOfType<GameObject>())
            {
                if (go.layer == 11)
                {
                    Destroy(go);
                }
            }

            for (int i = 0; i < CustomTrial.GlobalSettings.Waves.Count; i++)
            {
                Wave wave = CustomTrial.GlobalSettings.Waves[i];
                for (int j = 0; j < wave.Enemies.Count; j++)
                {
                    Enemy enemy = wave.Enemies[j];

                    string enemyName = enemy.Name.ToLower().Replace(" ", "");
                    GameObject spawnedEnemy = SpawnEnemy(enemyName, enemy.SpawnPosition);
                    if (enemy.Health > 0)
                    {
                        spawnedEnemy.GetComponent<HealthManager>().hp = enemy.Health;
                    }
                    _queuedEnemies.Add($"{i}:{enemyName}:{j}", spawnedEnemy);
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
            for (int i = 0; i < CustomTrial.GlobalSettings.Waves.Count; i++)
            {
                Wave wave = CustomTrial.GlobalSettings.Waves[i];
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
                        GameObject platform = Instantiate(CustomTrial.GameObjects["platform"], platSpawn, Quaternion.identity);
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

                if (wave.WallCDistance >= 0.0f)
                {
                    _wallCCtrl.Fsm.GetFsmFloat("Distance").Value = wave.WallCDistance;
                    _wallCCtrl.SendEvent("MOVE");
                }

                if (wave.WallLDistance >= 0.0f)
                {
                    _wallLCtrl.Fsm.GetFsmFloat("Distance").Value = wave.WallLDistance;
                    _wallLCtrl.SendEvent("MOVE");
                }

                if (wave.WallRDistance >= 0.0f)
                {
                    _wallRCtrl.Fsm.GetFsmFloat("Distance").Value = wave.WallRDistance;
                    _wallRCtrl.SendEvent("MOVE");
                }

                yield return new WaitUntil(() => _wallCCtrl.ActiveStateName == "Idle" && _wallLCtrl.ActiveStateName == "Idle" && _wallRCtrl.ActiveStateName == "Idle");

                float wallCY = _wallC.transform.Find("Wall").transform.position.y;
                float wallLX = _wallL.transform.Find("Wall").transform.position.x;
                float wallRX = _wallR.transform.Find("Wall").transform.position.x;
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

                    GameObject largeCage = Instantiate(CustomTrial.GameObjects["largecage"], enemy.SpawnPosition, Quaternion.identity);
                    largeCage.SetActive(true);
                    spawn = largeCage.LocateMyFSM("Spawn");

                    string enemyName = enemy.Name.ToLower().Replace(" ", "");
                    spawn.GetState("Spawn").RemoveAction<ActivateGameObject>();
                    spawn.GetState("Spawn").InsertMethod(1, () => _queuedEnemies[$"{waveNum}:{enemyName}:{enemyNum}"].SetActive(true));
                    spawn.SetState("Init");
                    spawn.SendEvent("SPAWN");

                    EnemyCount++;

                    yield return new WaitForSeconds(wave.DelayBetweenSpawns);
                }

                yield return new WaitWhile(() => EnemyCount > 0);

                switch (wave.CrowdAction)
                {
                    case "Cheer":
                        foreach (GameObject crowd in CustomTrial.Crowds)
                        {
                            crowd.LocateMyFSM("Control").SendEvent("CROWD CHEER");
                        }
                        _crowdAudioCtrl.SendEvent("CROWD SHORT CHEER");
                        break;
                    case "Gasp":
                        foreach (GameObject crowd in CustomTrial.Crowds)
                        {
                            crowd.LocateMyFSM("Control").SendEvent("CROWD GASP");
                        }
                        _crowdAudioCtrl.SendEvent("CROWD GASP");
                        break;
                    case "Laugh":
                        foreach (GameObject crowd in CustomTrial.Crowds)
                        {
                            crowd.LocateMyFSM("Control").SendEvent("CROWD LAUGH");
                        }
                        _crowdAudioCtrl.SendEvent("CROWD LAUGH");
                        break;
                }
            }

            foreach (Transform spike in _groundSpikes.transform)
                spike.gameObject.LocateMyFSM("Control").SendEvent("RETRACT");

            foreach (GameObject plat in _platforms)
                plat.LocateMyFSM("Control").SendEvent("SLOW RETRACT");


            _platforms.Clear();
            _platPos.Clear();

            _wallCCtrl.Fsm.GetFsmFloat("Distance").Value = 0;
            _wallLCtrl.Fsm.GetFsmFloat("Distance").Value = 0;
            _wallRCtrl.Fsm.GetFsmFloat("Distance").Value = 0;
            _wallCCtrl.SendEvent("MOVE");
            _wallLCtrl.SendEvent("MOVE");
            _wallRCtrl.SendEvent("MOVE");

            CustomTrial.Instance.Log("Battle Over!");
            
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
    }
}