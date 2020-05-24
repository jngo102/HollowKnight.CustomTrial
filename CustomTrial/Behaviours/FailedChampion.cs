using System.Collections;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class FailedChampion : MonoBehaviour
    {
        private PlayMakerFSM _control;

        private void Awake()
        {
            _control = gameObject.LocateMyFSM("FalseyControl");
        }

        private IEnumerator Start()
        {
            _control.SetState("Init");

            _control.Fsm.GetFsmFloat("Rage Point X").Value = ArenaInfo.CenterX;
            _control.Fsm.GetFsmFloat("Range Max").Value = ArenaInfo.RightX;
            _control.Fsm.GetFsmFloat("Range Min").Value = ArenaInfo.LeftX; 
                    
            yield return new WaitWhile(() => _control.ActiveStateName != "Dormant");

            _control.SendEvent("BATTLE START");
        }
    }
}