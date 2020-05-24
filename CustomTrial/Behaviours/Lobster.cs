using System.Collections;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class Lobster : MonoBehaviour
    {
        private PlayMakerFSM _control;

        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            _control.SetState("Init");

            yield return new WaitWhile(() => _control.ActiveStateName != "Dormant");

            _control.SendEvent("WAKE");
        }
    }
}