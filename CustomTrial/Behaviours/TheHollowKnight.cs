using System.Collections;
using CustomTrial.Utilities;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class TheHollowKnight : MonoBehaviour
    {
        private PlayMakerFSM _control;
        private PlayMakerFSM _phaseCtrl;
        
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
            _phaseCtrl = gameObject.LocateMyFSM("Phase Control");
        }

        private IEnumerator Start()
        {
            _control.SetState("Init");

            _control.Fsm.GetFsmFloat("Left X").Value = ArenaInfo.LeftX;
            _control.Fsm.GetFsmFloat("Right X").Value = ArenaInfo.RightX;
            _control.Fsm.GetFsmFloat("TeleRange Max").Value = ArenaInfo.RightX - 1;
            _control.Fsm.GetFsmFloat("TeleRange Min").Value = ArenaInfo.LeftX + 4;
            
            _control.GetAction<FloatInRange>("Pos", 1).lowerValue.Value = ArenaInfo.CenterX - 3;
            _control.GetAction<FloatInRange>("Pos", 1).upperValue.Value = ArenaInfo.CenterX;
            _control.GetAction<FloatInRange>("Pos", 2).lowerValue.Value = ArenaInfo.CenterX;
            _control.GetAction<FloatInRange>("Pos", 2).upperValue.Value = ArenaInfo.CenterX + 3;
            _control.GetAction<SetPosition>("Pos").x.Value = ArenaInfo.CenterX;
            _control.GetAction<FloatInRange>("Pos 2", 1).lowerValue.Value = ArenaInfo.CenterX - 3;
            _control.GetAction<FloatInRange>("Pos 2", 1).upperValue.Value = ArenaInfo.CenterX;
            _control.GetAction<FloatInRange>("Pos 2", 2).lowerValue.Value = ArenaInfo.CenterX;
            _control.GetAction<FloatInRange>("Pos 2", 2).upperValue.Value = ArenaInfo.CenterX + 3;
            _control.GetAction<SetPosition>("Pos 2").x.Value = ArenaInfo.CenterX;
            _control.GetAction<SetPosition>("Shift L").x.Value = ArenaInfo.CenterX - 3;
            _control.GetAction<SetPosition>("Shift L 2").x.Value = ArenaInfo.CenterX - 3;
            _control.GetAction<SetPosition>("Shift R").x.Value = ArenaInfo.CenterX + 3;
            _control.GetAction<SetPosition>("Shift R 2").x.Value = ArenaInfo.CenterX + 3;
            _control.GetAction<FloatClamp>("TelePos Dstab").minValue.Value = ArenaInfo.LeftX + 4;
            _control.GetAction<FloatClamp>("TelePos Dstab").maxValue.Value = ArenaInfo.RightX - 1;
            _control.GetAction<SetPosition>("TelePos Dstab").y.Value = ArenaInfo.BottomY + 6;
            
            _control.RemoveAction<SendEventByName>("Roar");
            _control.RemoveAction<SetFsmGameObject>("Roar");
            _control.RemoveAction<ApplyMusicCue>("HK Decline Music");
            _control.RemoveAction<PlayerDataBoolTest>("Long Roar End");

            _phaseCtrl.RemoveAction<PlayerDataBoolTest>("Set Phase 4");
            _phaseCtrl.RemoveAction<TransitionToAudioSnapshot>("HK DECLINE 2");
            _phaseCtrl.RemoveAction<TransitionToAudioSnapshot>("HK DECLINE 3");
            _phaseCtrl.RemoveAction("Die", 4);
            
            yield return null;
        }
    }
}