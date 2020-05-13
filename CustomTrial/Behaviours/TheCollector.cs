using System.Collections;
using CustomTrial.Utilities;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class TheCollector : MonoBehaviour
    {
        private PlayMakerFSM _control;
        
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            _control.SetState("Init");

            _control.Fsm.GetFsmFloat("Bottle XL").Value = ArenaInfo.LeftX;
            _control.Fsm.GetFsmFloat("Bottle XR").Value = ArenaInfo.RightX;
            _control.Fsm.GetFsmFloat("Roof XL").Value = ArenaInfo.LeftX + 2;
            _control.Fsm.GetFsmFloat("Roof XR").Value = ArenaInfo.RightX - 2;
            _control.Fsm.GetFsmFloat("Roof Y").Value = ArenaInfo.TopY + 5;
            
            _control.InsertMethod("Start Land", _control.GetState("Start Land").Actions.Length, () => _control.SetState("Roar Recover"));

            _control.SetState("Roar Recover");

            yield return null;
        }
    }
}