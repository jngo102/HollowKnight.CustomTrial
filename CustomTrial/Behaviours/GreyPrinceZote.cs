using System.Collections;
using ModCommon.Util;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class GreyPrinceZote : MonoBehaviour
    {
        private PlayMakerFSM _constrainX;
        private PlayMakerFSM _control;

        private void Awake()
        {
            _constrainX = gameObject.LocateMyFSM("Constrain X");
            _control = gameObject.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            _constrainX.Fsm.GetFsmFloat("Edge L").Value = ArenaInfo.LeftX;
            _constrainX.Fsm.GetFsmFloat("Edge R").Value = ArenaInfo.RightX;
            
            _control.SetState("Init");

            _control.Fsm.GetFsmFloat("Left X").Value = ArenaInfo.LeftX;
            _control.Fsm.GetFsmFloat("Right X").Value = ArenaInfo.RightX;
            
            _control.InsertMethod("Enter 1", _control.GetState("Enter 1").Actions.Length, () => _control.SetState("Idle Start"));
            
            yield return new WaitWhile(() => _control.ActiveStateName != "Dormant");

            _control.SendEvent("ZOTE APPEAR");
        }

        private void Update()
        {
            Modding.Logger.Log("[GPZ] " + _control.ActiveStateName);
        }
    }
}