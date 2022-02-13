using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    internal class JellyfishSpawner : MonoBehaviour
    {
        private PlayMakerFSM _spawn;

        private void Awake()
        {
            _spawn = gameObject.LocateMyFSM("Spawn");
        }

        private void Start()
        {
            _spawn.GetAction<RandomFloat>("Spawn", 0).min.Value = ArenaInfo.LeftX + 2;
            _spawn.GetAction<RandomFloat>("Spawn", 0).max.Value = ArenaInfo.RightX - 2;
            _spawn.GetAction<SetPosition>("Spawn", 2).y.Value = ArenaInfo.BottomY - 5;
            
            _spawn.GetAction<RandomFloat>("Spawn", 3).min.Value = ArenaInfo.LeftX + 2;
            _spawn.GetAction<RandomFloat>("Spawn", 3).max.Value = ArenaInfo.RightX - 2;
            _spawn.GetAction<SetPosition>("Spawn", 5).y.Value = ArenaInfo.BottomY - 8;
            
            _spawn.GetAction<RandomFloat>("Spawn", 6).min.Value = ArenaInfo.LeftX + 2;
            _spawn.GetAction<RandomFloat>("Spawn", 6).max.Value = ArenaInfo.RightX - 2;
            _spawn.GetAction<SetPosition>("Spawn", 8).y.Value = ArenaInfo.BottomY - 11;
            
            _spawn.GetAction<RandomFloat>("Spawn", 9).min.Value = ArenaInfo.LeftX + 2;
            _spawn.GetAction<RandomFloat>("Spawn", 9).max.Value = ArenaInfo.RightX - 2;
            _spawn.GetAction<SetPosition>("Spawn", 11).y.Value = ArenaInfo.BottomY - 14;

            _spawn.SetState("Init");
        }
    }
}