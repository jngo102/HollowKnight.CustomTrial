using System.Collections;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class MossKnight : MonoBehaviour
    {
        private PlayMakerFSM _control;

        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Moss Knight Control");
        }

        private IEnumerator Start()
        {
            _control.Fsm.GetFsmBool("Dormant").Value = false;
            
            _control.SetState("Initialise");

            yield return null;
        }
    }
}