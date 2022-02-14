using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Modding;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    internal class NoEyes : MonoBehaviour
    {
        private List<GameObject> _heads = new();

        private PlayMakerFSM _movement;
        private PlayMakerFSM _shotSpawn;

        private void Awake()
        {
            _movement = gameObject.LocateMyFSM("Movement");
            _shotSpawn = gameObject.LocateMyFSM("Shot Spawn");

            var corpse = ReflectionHelper.GetField<EnemyDeathEffects, GameObject>(GetComponent<EnemyDeathEffectsNoEffect>(), "corpse");
            corpse.LocateMyFSM("Control").GetState("End").RemoveAction<CreateObject>();
        }

        private void Start()
        {
            FsmState broadcastDeathSet = gameObject.LocateMyFSM("Broadcast Ghost Death").GetState("Set");
            broadcastDeathSet.RemoveAction<SendEventByName>();
            broadcastDeathSet.AddMethod(() =>
            {
                foreach (GameObject noEyesHead in _heads)
                {
                    if (noEyesHead == null) continue;
                    FSMUtility.SendEventToGameObject(noEyesHead, "GHOST DEATH");
                }
            });

            for (int index = 1; index <= 8; index++)
            {
                _movement.Fsm.GetFsmVector3($"P{index}").Value = RandomVector3();
            }

            _shotSpawn.GetAction<RandomFloat>("Spawn L", 1).min = ArenaInfo.BottomY;
            _shotSpawn.GetAction<RandomFloat>("Spawn L", 1).max = ArenaInfo.CenterY;
            _shotSpawn.GetAction<SetPosition>("Spawn L", 2).x = ArenaInfo.LeftX;
            _shotSpawn.GetAction<RandomFloat>("Spawn L", 5).min = ArenaInfo.CenterY;
            _shotSpawn.GetAction<RandomFloat>("Spawn L", 5).max = ArenaInfo.TopY;
            _shotSpawn.GetAction<SetPosition>("Spawn L", 6).x = ArenaInfo.RightX;

            _shotSpawn.GetAction<RandomFloat>("Spawn R", 1).min = ArenaInfo.BottomY;
            _shotSpawn.GetAction<RandomFloat>("Spawn R", 1).max = ArenaInfo.CenterY;
            _shotSpawn.GetAction<SetPosition>("Spawn R", 2).x = ArenaInfo.RightX;
            _shotSpawn.GetAction<RandomFloat>("Spawn R", 5).min = ArenaInfo.CenterY;
            _shotSpawn.GetAction<RandomFloat>("Spawn R", 5).max = ArenaInfo.TopY;
            _shotSpawn.GetAction<SetPosition>("Spawn R", 6).x = ArenaInfo.LeftX;

            _shotSpawn.GetState("Spawn L").InsertMethod(1, () => _heads.Add(_shotSpawn.Fsm.GetFsmGameObject("Shot").Value));
            _shotSpawn.GetState("Spawn L").InsertMethod(6, () => _heads.Add(_shotSpawn.Fsm.GetFsmGameObject("Shot").Value));
            _shotSpawn.GetState("Spawn R").InsertMethod(1, () => _heads.Add(_shotSpawn.Fsm.GetFsmGameObject("Shot").Value));
            _shotSpawn.GetState("Spawn R").InsertMethod(6, () => _heads.Add(_shotSpawn.Fsm.GetFsmGameObject("Shot").Value));
        }
        
        private Vector3 RandomVector3()
        {
            float x = Random.Range(ArenaInfo.LeftX, ArenaInfo.RightX);
            float y = Random.Range(ArenaInfo.BottomY, ArenaInfo.TopY);
            float z = 0.006f;

            return new Vector3(x, y, z);
        }
    }
}