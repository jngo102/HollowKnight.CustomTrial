using System.Collections;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    internal class SoulTyrant : MonoBehaviour
    {
        private PlayMakerFSM _lord;

        private void Awake()
        {
            _lord = gameObject.LocateMyFSM("Mage Lord");
        }

        private IEnumerator Start()
        {
            _lord.Fsm.GetFsmFloat("Bot Y").Value = ArenaInfo.BottomY + 3;
            _lord.Fsm.GetFsmFloat("Ground Y").Value = ArenaInfo.BottomY + 3;
            _lord.Fsm.GetFsmFloat("Knight Quake Y Max").Value = ArenaInfo.BottomY + 7.5f;
            _lord.Fsm.GetFsmFloat("Left X").Value = ArenaInfo.LeftX;
            _lord.Fsm.GetFsmFloat("Right X").Value = ArenaInfo.RightX;
            _lord.Fsm.GetFsmFloat("Quake Y").Value = ArenaInfo.BottomY + 10;
            _lord.Fsm.GetFsmFloat("Shockwave Y").Value = ArenaInfo.BottomY;
            _lord.Fsm.GetFsmFloat("Top Y").Value = ArenaInfo.BottomY + 8;
            
            _lord.SetState("Init");

            yield return new WaitUntil(() => _lord.ActiveStateName == "Sleep");

            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<HealthManager>().IsInvincible = false;
            _lord.SetState("Set Idle Timer");
        }
    }
}