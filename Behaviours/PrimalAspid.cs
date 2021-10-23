using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class PrimalAspid : MonoBehaviour
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