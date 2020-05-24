using System.Collections;
using ModCommon.Util;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class Nailmaster : MonoBehaviour
    {
        private PlayMakerFSM _nailmaster;

        private void Awake()
        {
            _nailmaster = gameObject.LocateMyFSM("nailmaster");
        }

        private IEnumerator Start()
        {
            _nailmaster.SetState("Init");
            
            _nailmaster.InsertMethod("Bow", 0, () => Destroy(gameObject, 3));
            
            yield return new WaitWhile(() => _nailmaster.ActiveStateName != "Rest" && _nailmaster.ActiveStateName != "Entry Wait");

            _nailmaster.SetState("Idle Stance");
        }
    }
}