using System.Collections;
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

        private IEnumerator Start()
        {
            _control.GetAction<FloatCompare>("In Air").float2 = ArenaInfo.BottomY + 2;
            
            _control.GetState("Music 2").RemoveAction<TransitionToAudioSnapshot>();

            _control.SetState("Init");

            yield return new WaitUntil(() => _control.ActiveStateName == "Sleep");

            _control.Fsm.GetFsmFloat("X Max").Value = ArenaInfo.RightX - 4;
            _control.Fsm.GetFsmFloat("X Min").Value = ArenaInfo.LeftX + 4;
            _control.Fsm.GetFsmFloat("Start Y").Value = ArenaInfo.BottomY + 2;
            
            _control.SetState("Submerge 1");
        }
    }
}