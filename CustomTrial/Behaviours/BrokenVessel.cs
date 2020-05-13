using System.Collections;
using CustomTrial.Utilities;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class BrokenVessel : MonoBehaviour
    {
        private PlayMakerFSM _control;

        private void Awake()
        {
            _control = gameObject.LocateMyFSM("IK Control");
        }

        private IEnumerator Start()
        {
            _control.SetState("Pause");

            _control.Fsm.GetFsmFloat("Left X").Value = ArenaInfo.LeftX;
            _control.Fsm.GetFsmFloat("Right X").Value = ArenaInfo.RightX;
            _control.Fsm.GetFsmFloat("Air Dash Height").Value = ArenaInfo.BottomY + 2;
            _control.Fsm.GetFsmFloat("Min Dstab Height").Value = ArenaInfo.BottomY + 4;

            _control.GetAction<SetPosition>("Set Pos").x = transform.position.x;
            _control.GetAction<SetPosition>("Set Pos").x = transform.position.y;

            yield return new WaitWhile(() => _control.ActiveStateName != "Intro Fall");

            _control.SetState("Roar End");
        }

        private void Update()
        {
            Modding.Logger.Log("IK Control State: " + _control.ActiveStateName);
        }
    }
}