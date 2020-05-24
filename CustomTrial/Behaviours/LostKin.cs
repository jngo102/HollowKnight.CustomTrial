using System.Collections;
using CustomTrial.Utilities;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class LostKin : MonoBehaviour
    {
        private PlayMakerFSM _control;

        private void Awake()
        {
            _control = gameObject.LocateMyFSM("IK Control");
        }

        private IEnumerator Start()
        {
            _control.SetState("Pause");

            _control.Fsm.GetFsmFloat("Air Dash Height").Value = ArenaInfo.BottomY + 3;
            _control.Fsm.GetFsmFloat("Left X").Value = ArenaInfo.LeftX;
            _control.Fsm.GetFsmFloat("Min Dstab Height").Value = ArenaInfo.BottomY + 5;
            _control.Fsm.GetFsmFloat("Right X").Value = ArenaInfo.RightX;
            
            _control.GetAction<SetPosition>("Intro Fall").x.Value = transform.position.x;
            _control.GetAction<SetPosition>("Intro Fall").y.Value = transform.position.y;
            _control.GetAction<SetPosition>("Set X", 0).x.Value = transform.position.x;
            _control.GetAction<SetPosition>("Set X", 2).x.Value = transform.position.x;

            yield return new WaitWhile(() => _control.ActiveStateName != "Intro Fall");

            _control.SetState("Roar End");
        }

        private void Update()
        {
            Modding.Logger.Log("[Lost Kin X] " + transform.position.x);
        }
    }
}