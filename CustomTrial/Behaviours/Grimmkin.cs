using System.Collections;
using CustomTrial.Utilities;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class Grimmkin : MonoBehaviour
    {
        public int grimmchildLevel;

        private Vector3 _offset;

        private PlayMakerFSM _control;

        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            _control.Fsm.GetFsmInt("Grimmchild Level").Value = grimmchildLevel;

            _control.InsertMethod("Follow", 8, () => _offset = HeroController.instance.transform.position - transform.position);
            
            _control.RemoveAction<ApplyMusicCue>("Explode");
            _control.RemoveAction<ApplyMusicCue>("Music");
            _control.RemoveAction<DistanceFlySmooth>("Follow");
            _control.RemoveAction<GetPlayerDataInt>("Init");

            _control.SetState("Init");
            
            yield return new WaitWhile(() => _control.ActiveStateName != "Init");

            _control.SendEvent("START");
            
        }

        private void FixedUpdate()
        {
            if (_control.ActiveStateName == "Follow")
            {
                Log("Offset: " + _offset);
                transform.SetPosition2D(HeroController.instance.transform.position - _offset);
            }
        }

        private void Log(object message) => Modding.Logger.Log("[Grimmkin] " + message);
    }
}