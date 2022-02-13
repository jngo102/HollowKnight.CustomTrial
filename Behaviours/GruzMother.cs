using System.Collections;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    internal class GruzMother : MonoBehaviour
    {
        private PlayMakerFSM _control;
        
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Big Fly Control");
        }

        private IEnumerator Start()
        {
            _control.GetState("Fly").RemoveAction<ApplyMusicCue>();
            
            _control.SetState("Init");

            GetComponent<HealthManager>().IsInvincible = false;

            yield return new WaitWhile(() => _control.ActiveStateName != "Invincible" && _control.ActiveStateName != "Wake");

            _control.SetState("Fly");
        }
    }
}