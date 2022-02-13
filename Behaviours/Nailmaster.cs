﻿using System.Collections;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    internal class Nailmaster : MonoBehaviour
    {
        private PlayMakerFSM _nailmaster;

        private void Awake()
        {
            _nailmaster = gameObject.LocateMyFSM("nailmaster");
        }

        private IEnumerator Start()
        {
            _nailmaster.SetState("Init");
            
            _nailmaster.GetState("Bow").InsertMethod(0, () => Destroy(gameObject, 3));
            
            yield return new WaitWhile(() => _nailmaster.ActiveStateName != "Rest" && _nailmaster.ActiveStateName != "Entry Wait");

            _nailmaster.SetState("Idle Stance");
        }
    }
}