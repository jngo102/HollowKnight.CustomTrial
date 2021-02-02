using System.Collections;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    public class Nosk : MonoBehaviour
    {
        private PlayMakerFSM _constrainX;
        private PlayMakerFSM _spider;

        private void Awake()
        {
            _constrainX = gameObject.LocateMyFSM("constrain_x");
            _spider = gameObject.LocateMyFSM("Mimic Spider");
        }

        private IEnumerator Start()
        {
            _constrainX.Fsm.GetFsmFloat("Edge L").Value = ArenaInfo.LeftX;
            _constrainX.Fsm.GetFsmFloat("Edge R").Value = ArenaInfo.RightX;

            _spider.Fsm.GetFsmFloat("Jump Max X").Value = ArenaInfo.RightX - 2;
            _spider.Fsm.GetFsmFloat("Jump Min X").Value = ArenaInfo.LeftX + 2;

            _spider.GetState("Trans 1").RemoveAction<ApplyMusicCue>();
            _spider.GetState("Trans 1").RemoveAction<CreateObject>();
            _spider.GetState("Trans 1").RemoveAction<SetFsmGameObject>();
            _spider.GetState("Trans 1").RemoveAction(8);
            
            _spider.SetState("Init");

            yield return new WaitWhile(() => _spider.ActiveStateName != "Hollow Idle");

            _spider.SetState("Roar End");

            yield return new WaitWhile(() => _spider.ActiveStateName != "Roar Init");

            _spider.SetState("Idle");
        }
    }
}