using System.Collections;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class EnragedGuardian : MonoBehaviour
    {
        private PlayMakerFSM _miner;
        
        private void Awake()
        {
            _miner = gameObject.LocateMyFSM("Beam Miner");
        }

        private IEnumerator Start()
        {
            _miner.SetState("Battle Init");

            _miner.Fsm.GetFsmFloat("Jump Max X").Value = ArenaInfo.LeftX;
            _miner.Fsm.GetFsmFloat("Jump Min X").Value = ArenaInfo.RightX;
            
            yield return null;
        }
    }
}