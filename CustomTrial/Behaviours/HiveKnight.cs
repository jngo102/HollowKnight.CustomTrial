using System.Collections;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class HiveKnight : MonoBehaviour
    {
        private PlayMakerFSM _control;

        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            _control.SetState("Init");

            yield return new WaitWhile(() => _control.ActiveStateName != "Sleep");

            GetComponent<MeshRenderer>().enabled = true;

            _control.Fsm.GetFsmFloat("Left X").Value = ArenaInfo.LeftX;
            _control.Fsm.GetFsmFloat("Right X").Value = ArenaInfo.RightX;
            _control.SetState("Activate");
        }
    }
}