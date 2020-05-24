using System.Collections;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class Flukemarm : MonoBehaviour
    {
        private PlayMakerFSM _mother;
        
        private void Awake()
        {
            _mother = gameObject.LocateMyFSM("Fluke Mother");
        }

        private IEnumerator Start()
        {
            _mother.SetState("Init");

            yield return new WaitUntil(() => _mother.ActiveStateName == "Idle");

            _mother.SetState("Roar End");
        }
    }
}