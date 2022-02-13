using System;
using System.Collections;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    internal class Dirtcarver : MonoBehaviour
    {
        private PlayMakerFSM _centipede;

        private void Awake()
        {
            _centipede = gameObject.LocateMyFSM("Centipede");
        }

        private IEnumerator Start()
        {
            _centipede.SetState("Init");
            
            yield return new WaitWhile(() => _centipede.ActiveStateName != "Dormant" && _centipede.ActiveStateName != "Emerge Antic");

            _centipede.SetState("Set Active");
            GetComponent<MeshRenderer>().enabled = true;
        }

        private void Update()
        {
            Modding.Logger.Log("Centipede: " + _centipede.ActiveStateName);
        }
    }
}