using System.Collections;
using HutongGames.PlayMaker.Actions;
using ModCommon.Util;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class PureVessel : MonoBehaviour
    {
        private const float GroundY = 6.4f;
        
        private PlayMakerFSM _control;
        
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            _control.SetState("Pause");
            
            var constrainPos = GetComponent<ConstrainPosition>();
            constrainPos.xMin = ArenaInfo.LeftX;
            constrainPos.xMax = ArenaInfo.RightX;           
            
            _control.Fsm.GetFsmFloat("Left X").Value = ArenaInfo.LeftX;
            _control.Fsm.GetFsmFloat("Right X").Value = ArenaInfo.RightX;
            _control.Fsm.GetFsmFloat("TeleRange Min").Value = ArenaInfo.LeftX + 2;
            _control.Fsm.GetFsmFloat("TeleRange Max").Value = ArenaInfo.RightX - 2;
            _control.Fsm.GetFsmFloat("Plume Y").Value = GroundY;
            _control.Fsm.GetFsmFloat("Stun Land Y").Value = GroundY + 20;

            _control.GetAction<FloatCompare>("Pos Check", 2).float2.Value = ArenaInfo.LeftX + 10;
            _control.GetAction<FloatCompare>("Pos Check", 3).float2.Value = ArenaInfo.RightX - 10;

            _control.RemoveAction("HUD Out", 0);
            
            yield return new WaitWhile(() => _control.ActiveStateName != "Intro 1");

            _control.SetState("Intro Roar End");
        }
    }
}