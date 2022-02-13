using System.Collections;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    internal class Lobster : MonoBehaviour
    {
        private PlayMakerFSM _control;

        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            _control.SetState("Init");
            
            yield return new WaitForEndOfFrame();
            //yield return new WaitUntil(() => _control.ActiveStateName == "Init");

            GetComponent<Rigidbody2D>().isKinematic = false;
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<PlayMakerFixedUpdate>().enabled = true;
            _control.SetState("Idle");
        }
    }
}