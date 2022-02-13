using System.Collections;
using UnityEngine;


namespace CustomTrial.Behaviours
{
    internal class Lancer : MonoBehaviour
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
            //_death.GetState("Set").InsertMethod(0, () => Destroy(gameObject, 3));
            _control.SetState("Init");

            yield return new WaitUntil(() => _control.ActiveStateName == "Launch");

            _control.SetState("Idle");
        }
    }
}