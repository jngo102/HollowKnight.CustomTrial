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
            _nosk.Fsm.GetFsmFloat("Y Max").Value = ArenaInfo.TopY;
            _nosk.Fsm.GetFsmFloat("Y Min").Value = ArenaInfo.BottomY;
            _nosk.Fsm.GetFsmFloat("Swoop Height").Value = ArenaInfo.CenterY;
            
            _nosk.GetAction<FloatCompare>("Swoop L").float2 = ArenaInfo.CenterX;
            _nosk.GetAction<FloatCompare>("Swoop R").float2 = ArenaInfo.CenterX;
            _nosk.GetAction<FloatCompare>("Shift Down?").float2 = ArenaInfo.CenterY;
            _nosk.GetAction<SetPosition>("Roof Impact").y = ArenaInfo.DefaultTopY + 2;
            _nosk.GetAction<SetPosition>("Roof Return").y = ArenaInfo.CenterY;
            
            _nosk.SetState("Init");

            yield return new WaitUntil(() => _nosk.ActiveStateName == "Dormant");

            _nosk.SetState("Idle");
        }
    }
}