using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    public class HornetProtector : MonoBehaviour
    {
        private PlayMakerFSM _control;

        private void Awake()
        {
            GetComponent<BoxCollider2D>().enabled = true;
            Destroy(gameObject.LocateMyFSM("Stun Control"));
            _control = gameObject.LocateMyFSM("Control");
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
            _control.Fsm.GetFsmFloat("Wall X Left").Value = ArenaInfo.LeftX - 1;
            _control.Fsm.GetFsmFloat("Wall X Right").Value = ArenaInfo.RightX + 1;

            _control.GetAction<BoolTestMulti>("Can Throw?", 4).boolVariables[0] = false;
            _control.GetAction<BoolTestMulti>("Can Throw?", 5).boolVariables[0] = false;

            var constrainPos = gameObject.GetComponent<ConstrainPosition>();
            constrainPos.constrainX = constrainPos.constrainY = false;
        }
    }
}