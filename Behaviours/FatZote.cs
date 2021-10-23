using HutongGames.PlayMaker.Actions;
using System.Collections;
using UnityEngine;
using Vasi;


namespace CustomTrial.Behaviours
{
    class FatZote : MonoBehaviour
    {
        private PlayMakerFSM _control;

        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
        }
        private IEnumerator Start()
        {
            _control.GetAction<RandomFloat>("Spawn Antic").min = ArenaInfo.LeftX;
            _control.GetAction<RandomFloat>("Spawn Antic").max = ArenaInfo.RightX;
            _control.GetAction<SetPosition>("Spawn Antic").y = transform.position.y;

            _control.SetState("Init");

            yield return new WaitUntil(() => _control.ActiveStateName == "Dormant");

            _control.SendEvent("SPAWN");
        }
    }
}