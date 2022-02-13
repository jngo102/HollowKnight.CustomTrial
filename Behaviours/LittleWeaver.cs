﻿using System.Collections;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    internal class LittleWeaver : MonoBehaviour
    {
        private PlayMakerFSM _control;

        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            _control.SetState("Initiate");

            yield return new WaitWhile(() => _control.ActiveStateName != "Spawn Wait");

            _control.SetState("Activate");
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
}