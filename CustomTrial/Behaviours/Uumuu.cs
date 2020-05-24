using System;
using System.Collections;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class Uumuu : MonoBehaviour
    {
        private PlayMakerFSM _bounds;
        private PlayMakerFSM _jellyfish;

        private void Awake()
        {
            _bounds = gameObject.LocateMyFSM("Bounds");
            _jellyfish = gameObject.LocateMyFSM("Mega Jellyfish");
        }

        private IEnumerator Start()
        {
            _bounds.Fsm.GetFsmFloat("X Max").Value = ArenaInfo.RightX;
            _bounds.Fsm.GetFsmFloat("X Min").Value = ArenaInfo.LeftX;
            _bounds.Fsm.GetFsmFloat("Y Max").Value = ArenaInfo.TopY;
            _bounds.Fsm.GetFsmFloat("Y Min").Value = ArenaInfo.BottomY;

            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = true;
            var constrainPosition = GetComponent<ConstrainPosition>();
            constrainPosition.xMax = ArenaInfo.RightX;
            constrainPosition.xMin = ArenaInfo.LeftX;
            constrainPosition.yMax = ArenaInfo.TopY;
            constrainPosition.yMin = ArenaInfo.BottomY;

            _jellyfish.SetState("Init");

            yield return new WaitUntil(() => _jellyfish.ActiveStateName == "Sleep");
            
            _jellyfish.SetState("Start");
        }
    }
}