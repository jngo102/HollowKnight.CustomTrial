﻿using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    internal class Gorb : MonoBehaviour
    {
        private PlayMakerFSM _movement;

        private GameObject _warp;
        private GameObject _warpOut;
        private GameObject _whiteFlash;
        
        private void Awake()
        {
            _movement = gameObject.LocateMyFSM("Movement");
            Destroy(gameObject.LocateMyFSM("Distance Attack"));
            
            _warp = transform.Find("Warp").gameObject;
            _warpOut = transform.Find("Warp Out").gameObject;
            _whiteFlash = transform.Find("White Flash").gameObject;
        }

        private void Start()
        {
            for (int i = 1; i <= 7; i++)
            {
                _movement.Fsm.GetFsmVector3("P" + i).Value = RandomVector3();
                _movement.GetAction<SetPosition>("Set " + i).gameObject.GameObject.Value.transform.position =
                    _movement.Fsm.GetFsmVector3("P" + i).Value;
            }

            var target = new GameObject("Target");
            target.transform.position = new Vector2(ArenaInfo.CenterX, ArenaInfo.CenterY);

            _movement.GetAction<ChaseObject>("Hover").target.Value = target;

            _movement.GetAction<FloatCompare>("Hover", 4).float2 = ArenaInfo.LeftX;
            _movement.GetAction<FloatCompare>("Hover", 5).float2 = ArenaInfo.RightX;
            _movement.GetAction<FloatCompare>("Hover", 6).float2 = ArenaInfo.CenterY;

            _movement.GetAction<SetPosition>("Return").x = ArenaInfo.CenterX;
            _movement.GetAction<SetPosition>("Return").y = ArenaInfo.CenterY;
            _movement.GetAction<ActivateGameObject>("Return", 1).gameObject.GameObject.Value = _warp;
            _movement.GetAction<ActivateGameObject>("Return", 2).gameObject.GameObject.Value = _whiteFlash;
            
            _movement.GetAction<FloatTestToBool>("Set Warp", 2).float2 = ArenaInfo.CenterX;
            _movement.GetAction<FloatTestToBool>("Set Warp", 3).float2 = ArenaInfo.CenterX;
            
            _movement.GetAction<ActivateGameObject>("Warp Out Fx").gameObject.GameObject.Value = _warpOut;
            
            _movement.GetAction<SetPosition>("Warp", 2).gameObject.GameObject.Value = _warpOut;
            _movement.GetAction<ActivateGameObject>("Warp", 3).gameObject.GameObject.Value = _warp;
            _movement.GetAction<ActivateGameObject>("Warp", 4).gameObject.GameObject.Value = _whiteFlash;
            
            _movement.GetAction<ActivateGameObject>("Warp Out", 0).gameObject.GameObject.Value = _warp;
            _movement.GetAction<ActivateGameObject>("Warp Out", 1).gameObject.GameObject.Value = _whiteFlash;

            _movement.SetState("Init");
        }

        private Vector3 RandomVector3()
        {
            float x = Random.Range(ArenaInfo.LeftX, ArenaInfo.RightX);
            float y = Random.Range(ArenaInfo.BottomY, ArenaInfo.CenterY);

            return new Vector3(x, y, .006f);
        }
    }
}