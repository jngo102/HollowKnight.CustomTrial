using System.Collections;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class WatcherKnight : MonoBehaviour
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
            
            yield return new WaitWhile(() => _knight.ActiveStateName != "Rest");

            _knight.SetState("Roar End");
        }

        private void Update()
        {
            Modding.Logger.Log("[Watcher Knight] " + _knight.ActiveStateName);
        }
    }
}