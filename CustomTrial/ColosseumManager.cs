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
        
        private PlayMakerFSM _battleCtrl;
        private PlayMakerFSM _manager;
        
        private void Awake()
        {
            _battleCtrl = gameObject.LocateMyFSM("Battle Control");
            _manager = gameObject.LocateMyFSM("Manager");
        }

        private IEnumerator Start()
        {
            Destroy(_battleCtrl);
            Destroy(gameObject.FindGameObjectInChildren("Waves"));

            foreach (GameObject go in FindObjectsOfType<GameObject>())
            {
                if (go.layer == 11)
                {
                    Destroy(go);
                }
            }

            yield return new WaitWhile(() => _manager.ActiveStateName != "Waves Start");
            
            //Wave wave1 = new Wave(false, 2.0f);
            
            /*wave1.AddEnemy(CustomTrial.GameObjects["Volatile Gruzzer"], new Vector2(100f, 10f));
            wave1.AddEnemy(CustomTrial.GameObjects["Husk Dandy"], new Vector2(95f, 10f));
            wave1.AddEnemy(CustomTrial.GameObjects["Dirtcarver"], new Vector2(105f, 12f));
            wave1.AddEnemy(CustomTrial.GameObjects["Vengefly"], new Vector2(105f, 12f));
            wave1.AddEnemy(CustomTrial.GameObjects["Fungoon"], new Vector2(105f, 12f));
            wave1.AddEnemy(CustomTrial.GameObjects["Fungified Husk A"], new Vector2(105f, 12f));*/
            //wave1.AddEnemy(CustomTrial.GameObjects["Menderbug"], new Vector2(105f, 12f));
            //wave1.AddEnemy(CustomTrial.GameObjects["Flukemunga"], new Vector2(105f, 8f));
            
            //_waves.Add(wave1);

            /*Wave wave2 = new Wave(false, 3.0f);
            wave2.AddEnemy(CustomTrial.GameObjects["Gorgeous Husk"], new Vector2(90, 7));
            wave2.AddEnemy(CustomTrial.GameObjects["Aspid Mother"], new Vector2(100, 7));
            wave2.AddEnemy(CustomTrial.GameObjects["Elder Baldur"], new Vector2(110, 10));

            _waves.Add(wave2);*/

            //Wave wave3 = new Wave(false, 1.0f);
            //wave3.AddEnemy(CustomTrial.GameObjects["Nightmare King Grimm"], new Vector2(90, 8));
            //wave3.AddEnemy(CustomTrial.GameObjects["Pure Vessel"], new Vector2(90, 8));
            //wave3.AddEnemy(CustomTrial.GameObjects["Absolute Radiance"], new Vector2(90, 10));
            
            //_waves.Add(wave3);
            
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

        private void Log(object message) => Modding.Logger.Log("[Colosseum Manager] " + message);
    }
}