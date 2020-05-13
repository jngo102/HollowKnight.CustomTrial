using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class Galien : MonoBehaviour
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