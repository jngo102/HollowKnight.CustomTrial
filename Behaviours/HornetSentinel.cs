using HutongGames.PlayMaker.Actions;
using Modding;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    internal class HornetSentinel : MonoBehaviour
    {
        private GameObject _barbRegion;

        private PlayMakerFSM _control;

        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");

            _barbRegion = Instantiate(CustomTrial.GameObjects["barbregion"]);
            _barbRegion.transform.SetPosition2D(ArenaInfo.CenterX, ArenaInfo.CenterY);
            _barbRegion.SetActive(true);

            ReflectionHelper.GetField<EnemyDeathEffects, GameObject>(GetComponent<EnemyDeathEffectsUninfected>(), "corpse").AddComponent<HornetCorpse>();

            GetComponent<HealthManager>().OnDeath += OnDeath;
        }

        private void Start()
        {
            _control.SetState("Pause");

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

            _control.GetAction<SetPosition>("Refight Wake").x = gameObject.transform.position.x;
            _control.GetAction<SetPosition>("Refight Wake").y = gameObject.transform.position.y;

            _control.GetState("Music").RemoveAction<ApplyMusicCue>();
            _control.GetState("Music").RemoveAction<TransitionToAudioSnapshot>();
            _control.GetState("Music (not GG)").RemoveAction<ApplyMusicCue>();
            _control.GetState("Music (not GG)").RemoveAction<TransitionToAudioSnapshot>();

            _control.GetState("Barb Throw").RemoveAction<SendEventByName>();
            _control.GetState("Barb Throw").AddMethod(() => _barbRegion.GetComponent<PlayMakerFSM>().SendEvent("SPAWN3"));

            var constrainPos = gameObject.GetComponent<ConstrainPosition>();
            constrainPos.constrainX = constrainPos.constrainY = false;
        }

        private void OnDeath()
        {
            Destroy(_barbRegion);
        }
    }
}