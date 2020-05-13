using System.Collections;
using CustomTrial.Utilities;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class NightmareKingGrimm : MonoBehaviour
    {
        private const float GroundY = 8.6f;
        
        private PlayMakerFSM _constrainX;
        private PlayMakerFSM _control;
        
        private void Awake()
        {
            _constrainX = gameObject.LocateMyFSM("constrain_x");
            _control = gameObject.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            _control.SetState("Init");
            
            yield return new WaitWhile(() => _control.ActiveStateName != "Dormant");
            
            _constrainX.Fsm.GetFsmFloat("Edge L").Value = ArenaInfo.LeftX;
            _constrainX.Fsm.GetFsmFloat("Edge R").Value = ArenaInfo.RightX;

            _control.Fsm.GetFsmFloat("Min X").Value = ArenaInfo.LeftX;
            _control.Fsm.GetFsmFloat("Max X").Value = ArenaInfo.RightX;
            _control.Fsm.GetFsmFloat("Ground Y").Value = GroundY;
            _control.Fsm.GetFsmFloat("Mid Y").Value = 13.0f;
            _control.GetAction<SetPosition>("Balloon Pos").x = (ArenaInfo.RightX - ArenaInfo.LeftX) / 2.0f;
            
            _control.SendEvent("TELE OUT");
        }
    }
}