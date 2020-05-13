using System.Collections;
using ModCommon.Util;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class BroodingMawlek : MonoBehaviour
    {
        private PlayMakerFSM _control;
        
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Mawlek Control");
        }

        private IEnumerator Start()
        {
            _control.SetState("Init");

            _control.InsertMethod("Wake Land", _control.GetState("Wake Land").Actions.Length,
                () => _control.SetState("Start"));
            
            yield return new WaitWhile(() => _control.ActiveStateName != "Dormant");
            
            _control.SendEvent("WAKE");
        }

        private void Update()
        {
            Modding.Logger.Log("[Mawlek] " + _control.ActiveStateName);
        }
    }
}