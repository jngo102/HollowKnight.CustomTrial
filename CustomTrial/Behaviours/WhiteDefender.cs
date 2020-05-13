using System.Collections;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class WhiteDefender : MonoBehaviour
    {
        private const float GroundY = 6.4f;
        
        private PlayMakerFSM _constrainX;
        private PlayMakerFSM _dd;
        
        private void Awake()
        {
            _constrainX = gameObject.LocateMyFSM("Constrain X");
            _dd = gameObject.LocateMyFSM("Dung Defender");
        }

        private IEnumerator Start()
        {
            _dd.SetState("Init");
            
            yield return new WaitWhile(() => _dd.ActiveStateName != "Sleep");

            _constrainX.Fsm.GetFsmFloat("Edge L").Value = ArenaInfo.LeftX;
            _constrainX.Fsm.GetFsmFloat("Edge R").Value = ArenaInfo.RightX;
            
            _dd.Fsm.GetFsmFloat("Centre X").Value = (ArenaInfo.RightX - ArenaInfo.LeftX) / 2.0f;
            _dd.Fsm.GetFsmFloat("Dolphin Max X").Value = ArenaInfo.RightX - 4 ;
            _dd.Fsm.GetFsmFloat("Dolphin Min X").Value = ArenaInfo.LeftX + 4;
            _dd.Fsm.GetFsmFloat("Erupt Y").Value = GroundY;
            _dd.Fsm.GetFsmFloat("Max X").Value = ArenaInfo.RightX;
            _dd.Fsm.GetFsmFloat("Min X").Value = ArenaInfo.LeftX;
            
            _dd.SetState("Will Evade?");
            
            Destroy(GameObject.Find("Dream Fall Catcher"));
        }
    }
}