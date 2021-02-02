using System.Collections;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    public class WingedNosk : MonoBehaviour
    {
        private PlayMakerFSM _nosk;

        private void Awake()
        {
            _nosk = gameObject.LocateMyFSM("Hornet Nosk");
        }

        private IEnumerator Start()
        {
            _nosk.Fsm.GetFsmFloat("X Max").Value = ArenaInfo.RightX;
            _nosk.Fsm.GetFsmFloat("X Min").Value = ArenaInfo.LeftX;
            
            _nosk.GetAction<FloatCompare>("Swoop L").float2.Value = ArenaInfo.LeftX + 5;
            _nosk.GetAction<FloatCompare>("Swoop R").float2.Value = ArenaInfo.RightX - 5;
            
            _nosk.SetState("Init");

            yield return new WaitWhile(() => _nosk.ActiveStateName != "Dormant");

            _nosk.SetState("Idle");
        }
    }
}