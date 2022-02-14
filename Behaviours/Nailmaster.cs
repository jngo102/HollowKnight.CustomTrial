using HutongGames.PlayMaker.Actions;
using System.Collections;
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

            Destroy(GetComponent<ConstrainPosition>());
        }

        private IEnumerator Start()
        {
            _nailmaster.SetState("Init");

            _nailmaster.Fsm.GetFsmBool("Brothered").Value = false;
            _nailmaster.Fsm.GetFsmBool("Phase 2").Value = true;

            _nailmaster.GetAction<FloatCompare>("Jump Antic").float2 = ArenaInfo.CenterX;
            _nailmaster.GetAction<FloatTestToBool>("Can Evade?", 4).float2 = ArenaInfo.LeftX + 2;
            _nailmaster.GetAction<FloatTestToBool>("Can Evade?", 5).float2 = ArenaInfo.RightX - 2;
            _nailmaster.GetAction<RandomFloat>("Aim Jump 2").min = ArenaInfo.LeftX + 6;
            _nailmaster.GetAction<RandomFloat>("Aim Jump 2").max = ArenaInfo.RightX - 6;
            _nailmaster.GetAction<SetFloatValue>("Dash L", 0).floatValue = ArenaInfo.RightX - 2;
            _nailmaster.GetAction<SetFloatValue>("Dash L", 1).floatValue = ArenaInfo.LeftX + 8;
            _nailmaster.GetAction<SetFloatValue>("Dash R", 0).floatValue = ArenaInfo.LeftX + 2;
            _nailmaster.GetAction<SetFloatValue>("Dash R", 1).floatValue = ArenaInfo.RightX - 8;

            _nailmaster.GetState("Defeated").AddMethod(() => _nailmaster.SendEvent("BOW"));
            _nailmaster.GetState("End").AddCoroutine(TweenOut);
            
            yield return new WaitUntil(() => _nailmaster.ActiveStateName == "Rest" || _nailmaster.ActiveStateName == "Entry Wait");

            _nailmaster.SetState("Idle");

            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<HealthManager>().IsInvincible = false;
            GetComponent<NonBouncer>().active = false;
            GetComponent<Rigidbody2D>().isKinematic = false;
        }

        private IEnumerator TweenOut()
        {
            yield return new WaitForSeconds(3);

            GetComponent<BoxCollider2D>().isTrigger = true;
            yield return new WaitUntil(() =>
            {
                transform.Translate(Vector3.down * 25 * Time.deltaTime);
                return transform.position.y <= ArenaInfo.BottomY - 10;
            });

            ColosseumManager.EnemyCount--;
            Destroy(gameObject);
        }
    }
}