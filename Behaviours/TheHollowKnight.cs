using System.Collections;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    internal class TheHollowKnight : MonoBehaviour
    {
        private PlayMakerFSM _control;
        private PlayMakerFSM _phaseCtrl;
        
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
            _phaseCtrl = gameObject.LocateMyFSM("Phase Control");
        }

        private void Start()
        {
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
            
            _control.GetState("Roar").RemoveAction<SendEventByName>();
            _control.GetState("Roar").RemoveAction<SetFsmGameObject>();
            _control.GetState("HK Decline Music").RemoveAction<ApplyMusicCue>();
            _control.GetState("Long Roar").RemoveAction<SendEventByName>();
            _control.GetState("Long Roar").RemoveAction<SetFsmGameObject>();
            _control.GetState("Long Roar End").RemoveAction<PlayerDataBoolTest>();

            _phaseCtrl.GetState("Set Phase 4").RemoveAction<PlayerDataBoolTest>();
            _phaseCtrl.GetState("HK DECLINE 2").RemoveAction<TransitionToAudioSnapshot>();
            _phaseCtrl.GetState("HK DECLINE 3").RemoveAction<TransitionToAudioSnapshot>();

            GameObject corpse = gameObject.transform.Find("Boss Corpse").gameObject;
            corpse.LocateMyFSM("Corpse").GetState("Burst").RemoveAction<SendEventByName>();
            corpse.LocateMyFSM("Corpse").GetState("Blow").InsertMethod(9, () =>
            {
                GameCameras.instance.StopCameraShake();
                ColosseumManager.EnemyCount--;
                Destroy(corpse);
                Destroy(gameObject);
            });

            _control.SetState("Init");
        }
    }
}