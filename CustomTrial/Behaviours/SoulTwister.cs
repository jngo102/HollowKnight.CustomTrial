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
            
            yield return null;
            
            //yield return new WaitUntil(() => _mage.ActiveStateName == "Sleep" || _mage.ActiveStateName == "Manual Sleep");
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
            
            _mage.SetState("Idle After Tele");
        }

        private void Update()
        {
            Modding.Logger.Log("[Mage] " + _mage.ActiveStateName);
        }
    }
}