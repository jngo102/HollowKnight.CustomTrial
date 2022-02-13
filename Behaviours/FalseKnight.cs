using HutongGames.PlayMaker;
using System.Collections;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    internal class FalseKnight : MonoBehaviour
    {
        private PlayMakerFSM _control;

        private void Awake()
        {
            _control = gameObject.LocateMyFSM("FalseyControl");
        }

        private IEnumerator Start()
        {
            _control.SetState(_control.Fsm.StartState);

            _control.Fsm.GetFsmFloat("Rage Point X").Value = ArenaInfo.CenterX;
            _control.Fsm.GetFsmFloat("Range Max").Value = ArenaInfo.RightX;
            _control.Fsm.GetFsmFloat("Range Min").Value = ArenaInfo.LeftX;

            _control.GetState("Check If GG").ChangeTransition("FINISHED", "Death Open");

            _control.GetState("Music").GetAction<GGCheckIfBossScene>().regularSceneEvent = FsmEvent.Finished;

            _control.GetState("Cough").RemoveAction<SendHealthManagerDeathEvent>();

            yield return new WaitWhile(() => _control.ActiveStateName != "Dormant");

            _control.SendEvent("BATTLE START");

            _control.GetState("Cough").InsertCoroutine(0, TweenOut);

            GetComponent<MeshRenderer>().enabled = true;
        }

        private IEnumerator TweenOut()
        {
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