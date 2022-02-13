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
            _control.GetAction<PlayerDataBoolTest>("Init").isFalse = FsmEvent.FindEvent("SUBMERGE");

            _control.SetState("Init");

            _control.Fsm.GetFsmFloat("X Max").Value = ArenaInfo.RightX;
            _control.Fsm.GetFsmFloat("X Min").Value = ArenaInfo.LeftX;
            _control.Fsm.GetFsmFloat("Start Y").Value = ArenaInfo.BottomY + 2.5f;

            transform.Find("Attack Range").GetComponent<BoxCollider2D>().size *= Vector2.right * 2;
        }
    }
}