using UnityEngine;

namespace CustomTrial.Behaviours
{
    internal class PrimalAspid : MonoBehaviour
    {
        private PlayMakerFSM _fsm;
        
        private void Awake()
        {
            _fsm = GetComponent<PlayMakerFSM>();
        }

        private void Start()
        {
            _fsm.SetState("Init");
        }
    }
}