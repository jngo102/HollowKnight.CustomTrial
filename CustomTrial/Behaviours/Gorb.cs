using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    public class Gorb : MonoBehaviour
    {
        private PlayMakerFSM _movement;

        private void Awake()
        {
            _movement = gameObject.LocateMyFSM("Movement");
        }

        private void Start()
        {
            _movement.Fsm.GetFsmVector3("P1").Value = RandomVector3();
            _movement.Fsm.GetFsmVector3("P2").Value = RandomVector3();
            _movement.Fsm.GetFsmVector3("P3").Value = RandomVector3();
            _movement.Fsm.GetFsmVector3("P4").Value = RandomVector3();
            _movement.Fsm.GetFsmVector3("P5").Value = RandomVector3();
            _movement.Fsm.GetFsmVector3("P6").Value = RandomVector3();
            _movement.Fsm.GetFsmVector3("P7").Value = RandomVector3();

            _movement.GetAction<FloatTestToBool>("Set Warp", 2).float2 = ArenaInfo.CenterX;
            _movement.GetAction<FloatTestToBool>("Set Warp", 3).float2 = ArenaInfo.CenterX;
            _movement.GetAction<SetPosition>("Return").x = ArenaInfo.CenterX;
            _movement.GetAction<SetPosition>("Return").y = ArenaInfo.CenterY;

            _movement.SetState("Init");
        }

        private Vector3 RandomVector3()
        {
            float x = Random.Range(ArenaInfo.LeftX, ArenaInfo.RightX);
            float y = Random.Range(ArenaInfo.BottomY, ArenaInfo.CenterY);
            float z = 0.006f;

            return new Vector3(x, y, z);
        }
    }
}