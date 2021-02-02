using System.Collections;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    public class Lancer : MonoBehaviour
    {
        private PlayMakerFSM _control;
        private PlayMakerFSM _death;

        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
            _death = gameObject.LocateMyFSM("Death Detect");
        }

        private IEnumerator Start()
        {
            _control.SetState("Init");

            _death.GetState("Set").InsertMethod(0, () => Destroy(gameObject, 3));
            
            yield return new WaitWhile(() => _control.ActiveStateName != "Launch");

            _control.SetState("Idle");
        }
    }
}