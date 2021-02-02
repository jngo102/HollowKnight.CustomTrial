using System.Collections;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class SoulTwister : MonoBehaviour
    {
        private PlayMakerFSM _mage;

        private void Awake()
        {
            _mage = gameObject.LocateMyFSM("Mage");
        }

        private IEnumerator Start()
        {
            _mage.SetState("Init");
            
            yield return new WaitUntil(() => _mage.ActiveStateName == "Manual Sleep");

            _mage.SetState("Wake");
        }
    }
}