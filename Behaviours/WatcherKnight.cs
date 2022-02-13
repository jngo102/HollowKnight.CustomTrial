using System.Collections;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    internal class WatcherKnight : MonoBehaviour
    {
        private PlayMakerFSM _knight;

        private void Awake()
        {
            _knight = gameObject.LocateMyFSM("Black Knight");
        }

        private IEnumerator Start()
        {
            _knight.SetState("Init");

            GetComponent<Rigidbody2D>().isKinematic = false;
            
            yield return new WaitUntil(() => _knight.ActiveStateName == "Rest");

            _knight.SetState("Roar End");
        }
    }
}