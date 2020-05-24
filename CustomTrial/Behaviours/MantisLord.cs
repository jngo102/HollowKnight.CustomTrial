using System;
using System.Collections;
using CustomTrial.Utilities;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class MantisLord : MonoBehaviour
    {
        private PlayMakerFSM _lord;

        private void Awake()
        {
            _lord = gameObject.LocateMyFSM("Mantis Lord");
        }

        private IEnumerator Start()
        {
            _lord.SetState("Init");

            //_lord.Fsm.GetFsmBool("Sub").Value = true;
            
            _lord.Fsm.GetFsmFloat("Dash Hero L").Value = ArenaInfo.CenterX - 0.1f;
            _lord.Fsm.GetFsmFloat("Dash Hero R").Value = ArenaInfo.CenterX + 0.1f;
            _lord.Fsm.GetFsmFloat("Dash X L").Value = ArenaInfo.CenterX - 8;
            _lord.Fsm.GetFsmFloat("Dash X R").Value = ArenaInfo.CenterX + 8;
            _lord.Fsm.GetFsmFloat("Dash Y").Value = ArenaInfo.BottomY;
            _lord.Fsm.GetFsmFloat("Dstab X Max").Value = ArenaInfo.RightX;
            _lord.Fsm.GetFsmFloat("Dstab X Min").Value = ArenaInfo.LeftX;
            _lord.Fsm.GetFsmFloat("Land Y").Value = ArenaInfo.BottomY;
            _lord.Fsm.GetFsmFloat("Throw Hero L").Value = ArenaInfo.CenterX - 8;
            _lord.Fsm.GetFsmFloat("Throw Hero R").Value = ArenaInfo.CenterX + 8;
            _lord.Fsm.GetFsmFloat("Wall X L").Value = ArenaInfo.LeftX - 1;
            _lord.Fsm.GetFsmFloat("Wall X R").Value = ArenaInfo.RightX + 1;
            _lord.Fsm.GetFsmFloat("Wall Y Max").Value = ArenaInfo.TopY - 1;
            _lord.Fsm.GetFsmFloat("Wall Y Min").Value = ArenaInfo.BottomY + 1;
            
            _lord.GetAction<FloatCompare>("Attack Choice", 2).float2.Value = ArenaInfo.BottomY + 3;
            _lord.GetAction<FloatCompare>("Attack Choice", 3).float2.Value = ArenaInfo.LeftX;
            _lord.GetAction<FloatCompare>("Attack Choice", 4).float2.Value = ArenaInfo.RightX;
            _lord.GetAction<FloatClamp>("Attack Choice").minValue.Value = ArenaInfo.BottomY;
            _lord.GetAction<FloatClamp>("Attack Choice").maxValue.Value = ArenaInfo.TopY - 4;
            
            _lord.RemoveAction<SendEvent>("Attack Choice");
            
            yield return null;
        }
    }
}