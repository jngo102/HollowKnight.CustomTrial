using System.Collections;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    public class MassiveMossCharger : MonoBehaviour
    {
        private PlayMakerFSM _control;
        
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Mossy Control");
        }

        private void Start()
        {
            _control.GetAction<FloatCompare>("In Air").float2 = ArenaInfo.BottomY + 2.5f;
            _control.GetState("Music").GetAction<GGCheckIfBossSequence>().falseEvent = FsmEvent.FindEvent("FINISHED");
            _control.GetState("Music 2").RemoveAction<TransitionToAudioSnapshot>();
            _control.GetState("Init").GetAction<GGCheckIfBossScene>().regularSceneEvent = FsmEvent.FindEvent("GG BOSS");

            _control.GetState("Roar").RemoveAction<SetFsmGameObject>();
            _control.GetState("Roar").RemoveAction<SendEventByName>();

            _control.Fsm.GetFsmFloat("X Max").Value = ArenaInfo.RightX;
            _control.Fsm.GetFsmFloat("X Min").Value = ArenaInfo.LeftX;
            _control.Fsm.GetFsmFloat("Start Y").Value = ArenaInfo.BottomY + 2;

            _control.SetState(_control.Fsm.StartState);
        }
    }
}