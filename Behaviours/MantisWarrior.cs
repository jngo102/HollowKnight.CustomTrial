using System.Collections;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class MantisWarrior : MonoBehaviour
    {
        private PlayMakerFSM _mantis;

        private void Awake()
        {
            _mantis = gameObject.LocateMyFSM("Mantis");
        }

        private IEnumerator Start()
        {
            _mantis.SetState("Init");
            
            yield return new WaitWhile(() => _mantis.ActiveStateName != "Friendly Idle" && _mantis.ActiveStateName != "Bow" && _mantis.ActiveStateName != "Anim");

            _mantis.SetState("Idle");
        }
    }
}