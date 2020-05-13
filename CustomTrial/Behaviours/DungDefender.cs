using System.Collections;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class DungDefender : MonoBehaviour
    {
        private PlayMakerFSM _dd;

        private void Awake()
        {
            _dd = gameObject.LocateMyFSM("Dung Defender");
        }

        private IEnumerator Start()
        {
            _dd.SetState("Init");

            GetComponent<MeshRenderer>().enabled = true;
            
            yield return new WaitWhile(() => _dd.ActiveStateName != "Sleep");
            
            _dd.SetState("Will Evade?");
        }
    }
}