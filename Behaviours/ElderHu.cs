using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    public class ElderHu : MonoBehaviour
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

            _movement.GetAction<FloatCompare>("Choose L").float2 = ArenaInfo.CenterX - 5;
            _movement.GetAction<FloatCompare>("Choose R").float2 = ArenaInfo.CenterX + 5;
            _movement.GetAction<FloatCompare>("Set Warp").float2 = ArenaInfo.CenterX;
            _movement.GetAction<SetVector3XYZ>("Choose L").x = ArenaInfo.LeftX + 2;
            _movement.GetAction<SetVector3XYZ>("Choose L").y = transform.position.y;
            _movement.GetAction<SetVector3XYZ>("Choose R").x = ArenaInfo.RightX - 2;
            _movement.GetAction<SetVector3XYZ>("Choose R").y = transform.position.y;
            _movement.GetAction<SetPosition>("Return").x = ArenaInfo.CenterX;
            _movement.GetAction<SetPosition>("Return").y = ArenaInfo.CenterY;
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