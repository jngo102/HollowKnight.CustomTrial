using System.Collections;
using CustomTrial.Utilities;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class GruzMother : MonoBehaviour
    {
        private PlayMakerFSM _control;
        
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Big Fly Control");
        }

        private IEnumerator Start()
        {
            _control.RemoveAction<ApplyMusicCue>("Fly");
            
            _control.SetState("Init");

            GetComponent<HealthManager>().IsInvincible = false;

            yield return new WaitWhile(() => _control.ActiveStateName != "Invincible" && _control.ActiveStateName != "Wake");

            _control.SetState("Fly");
        }
        
        private void Update()
        {
            Modding.Logger.Log("[Gruz Mother] " + _control.ActiveStateName);
        }
    }
}