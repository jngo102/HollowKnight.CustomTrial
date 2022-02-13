using HutongGames.PlayMaker.Actions;
using Modding;
using System.Collections;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    internal class HornetProtector : MonoBehaviour
    {
        private PlayMakerFSM _control;

        private void Awake()
        {
            GetComponent<BoxCollider2D>().enabled = true;
            _control = gameObject.LocateMyFSM("Control");

            ReflectionHelper.GetField<EnemyDeathEffects, GameObject>(GetComponent<EnemyDeathEffectsUninfected>(), "corpse").AddComponent<HornetCorpse>();
        }

        private void Start()
        {
            _control.SetState("Pause");

            _control.GetState("Music").RemoveAction<TransitionToAudioSnapshot>();
            _control.GetState("Music").RemoveAction<ApplyMusicCue>();

            _control.Fsm.GetFsmFloat("Air Dash Height").Value = ArenaInfo.BottomY + 4;
            _control.Fsm.GetFsmFloat("Floor Y").Value = ArenaInfo.BottomY;
            _control.Fsm.GetFsmFloat("Left X").Value = ArenaInfo.LeftX;
            _control.Fsm.GetFsmFloat("Min Dstab Height").Value = ArenaInfo.BottomY + 6;
            _control.Fsm.GetFsmFloat("Right X").Value = ArenaInfo.RightX;
            _control.Fsm.GetFsmFloat("Roof Y").Value = ArenaInfo.TopY;
            _control.Fsm.GetFsmFloat("Sphere Y").Value = ArenaInfo.BottomY + 6;
            _control.Fsm.GetFsmFloat("Throw X L").Value = ArenaInfo.LeftX + 6.5f;
            _control.Fsm.GetFsmFloat("Throw X R").Value = ArenaInfo.RightX - 6.5f;
            _control.Fsm.GetFsmFloat("Wall X Left").Value = ArenaInfo.LeftX + 1;
            _control.Fsm.GetFsmFloat("Wall X Right").Value = ArenaInfo.RightX - 1;

            var constrainPos = gameObject.GetComponent<ConstrainPosition>();
            constrainPos.constrainX = constrainPos.constrainY = false;
        }
    }

    internal class HornetCorpse : MonoBehaviour
    {
        private void Start()
        {
            gameObject.LocateMyFSM("Control").GetState("Land").AddCoroutine(TweenOut);
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

            Destroy(gameObject);
        }
    }
}