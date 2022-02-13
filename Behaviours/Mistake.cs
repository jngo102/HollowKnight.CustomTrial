using System.Collections;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    internal class Mistake : MonoBehaviour
    {
        private PlayMakerFSM _blob;

        private void Awake()
        {
            _blob = gameObject.LocateMyFSM("Blob");
        }

        private IEnumerator Start()
        {
            _blob.SetState("Init");

            yield return new WaitWhile(() => _blob.ActiveStateName != "Hide");

            _blob.SetState("Activate");
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
}