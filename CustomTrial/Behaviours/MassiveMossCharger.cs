using System.Collections;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class MassiveMossCharger : MonoBehaviour
    {
        private PlayMakerFSM _control;
        
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Mossy Control");
        }

        private IEnumerator Start()
        {
            _control.SetState("Init");
            
            yield return new WaitWhile(() => _control.ActiveStateName != "Init");

            _control.SendEvent("SUBMERGE");
        }
    }
}