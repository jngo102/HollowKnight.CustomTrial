using System.Collections;
using System.Linq;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    public class TroupeMasterGrimm : MonoBehaviour
    {
        private PlayMakerFSM _constrainX;
        private PlayMakerFSM _constrainY;
        private PlayMakerFSM _control;

        private void Awake()
        {
            _constrainX = gameObject.LocateMyFSM("constrain_x");
            _constrainY = gameObject.LocateMyFSM("Constrain Y");
            _control = gameObject.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            _constrainX.Fsm.GetFsmFloat("Edge L").Value = ArenaInfo.LeftX + 1;
            _constrainX.Fsm.GetFsmFloat("Edge R").Value = ArenaInfo.RightX - 1;

            _constrainY.GetAction<FloatCompare>("Check").float2.Value = ArenaInfo.BottomY;
            _constrainY.GetAction<SetFloatValue>("Constrain").floatValue.Value = ArenaInfo.BottomY;

            _control.Fsm.GetFsmFloat("AD Max X").Value = ArenaInfo.RightX - 1;
            _control.Fsm.GetFsmFloat("AD Min X").Value = ArenaInfo.LeftX + 5;
            _control.Fsm.GetFsmFloat("Ground Y").Value = ArenaInfo.BottomY + 2;
            _control.Fsm.GetFsmFloat("Max X").Value = ArenaInfo.RightX;
            _control.Fsm.GetFsmFloat("Min X").Value = ArenaInfo.LeftX;

            _control.GetAction<SetPosition>("Balloon Pos").x = ArenaInfo.CenterX;
            _control.GetAction<SetPosition>("Balloon Pos").y = 14.0f;

            _control.GetState("Death Start").InsertMethod(0, () =>
            {
                Destroy(FindObjectsOfType<GameObject>().First(go => go.name.Contains("Grimm Spike Holder")));
                ColosseumManager.EnemyCount--;
            });

            _control.GetState("Bow").RemoveAction<ApplyMusicCue>();
            _control.GetState("Bow").RemoveAction<TransitionToAudioSnapshot>();

            _control.SetState("Init");

            yield return new WaitUntil(() => _control.ActiveStateName == "Bow");

            _control.SetState("Tele Out");
        }
    }
}