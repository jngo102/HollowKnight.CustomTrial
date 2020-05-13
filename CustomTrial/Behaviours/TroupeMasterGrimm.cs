using System.Collections;
using CustomTrial.Utilities;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class TroupeMasterGrimm : MonoBehaviour
    {
        private PlayMakerFSM _constrainX;
        private PlayMakerFSM _constrainY;
        private PlayMakerFSM _control;

        private void Awake()
        {
            _constrainX = gameObject.LocateMyFSM("constrain_x");
            _constrainY = gameObject.LocateMyFSM("Constrain Y");
            _control = gameObject.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            _constrainX.Fsm.GetFsmFloat("Edge L").Value = ArenaInfo.LeftX;
            _constrainX.Fsm.GetFsmFloat("Edge R").Value = ArenaInfo.RightX;

            _constrainY.GetAction<FloatCompare>("Check").float2.Value = ArenaInfo.BottomY;
            _constrainY.GetAction<SetFloatValue>("Constrain").floatValue.Value = ArenaInfo.BottomY;
            
            _control.SetState("Init");
            
            _control.Fsm.GetFsmFloat("AD Max X").Value = ArenaInfo.RightX - 1;
            _control.Fsm.GetFsmFloat("AD Min X").Value = ArenaInfo.LeftX + 5;
            _control.Fsm.GetFsmFloat("Ground Y").Value = ArenaInfo.BottomY;
            _control.Fsm.GetFsmFloat("Max X").Value = ArenaInfo.RightX;
            _control.Fsm.GetFsmFloat("Min X").Value = ArenaInfo.LeftX;

            _control.GetAction<SetPosition>("Balloon Pos").x = (ArenaInfo.RightX - ArenaInfo.LeftX) / 2.0f;
            _control.GetAction<SetPosition>("Balloon Pos").y = 14.0f;
            
            _control.RemoveAction<ApplyMusicCue>("Bow");

            
            yield return null;
        }
    }
}