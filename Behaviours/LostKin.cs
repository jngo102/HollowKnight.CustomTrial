﻿using System.Collections;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    internal class LostKin : MonoBehaviour
    {
        private PlayMakerFSM _control;
        private PlayMakerFSM _spawn;

        private void Awake()
        {
            _control = gameObject.LocateMyFSM("IK Control");
            _spawn = gameObject.LocateMyFSM("Spawn Balloon");
        }

        private IEnumerator Start()
        {
            _control.SetState("Pause");

            _control.Fsm.GetFsmFloat("Air Dash Height").Value = ArenaInfo.BottomY + 3;
            _control.Fsm.GetFsmFloat("Left X").Value = ArenaInfo.LeftX;
            _control.Fsm.GetFsmFloat("Min Dstab Height").Value = ArenaInfo.BottomY + 5;
            _control.Fsm.GetFsmFloat("Right X").Value = ArenaInfo.RightX;

            _control.GetAction<RandomFloat>("Aim Jump 2").min = ArenaInfo.CenterX - 1;
            _control.GetAction<RandomFloat>("Aim Jump 2").max = ArenaInfo.CenterX + 1;
            _control.GetAction<SetPosition>("Intro Fall").x = transform.position.x;
            _control.GetAction<SetPosition>("Intro Fall").y = transform.position.y;
            _control.GetAction<SetPosition>("Set X", 0).x = transform.position.x;
            _control.GetAction<SetPosition>("Set X", 2).x = transform.position.x;

            _spawn.Fsm.GetFsmFloat("X Min").Value = ArenaInfo.LeftX + 1;
            _spawn.Fsm.GetFsmFloat("X Max").Value = ArenaInfo.RightX - 1;
            _spawn.Fsm.GetFsmFloat("Y Min").Value = ArenaInfo.BottomY + 1;
            _spawn.Fsm.GetFsmFloat("Y Max").Value = ArenaInfo.BottomY + 5;

            yield return new WaitUntil(() => _control.ActiveStateName == "Intro Fall");

            _control.SetState("Roar End");
        }
    }
}