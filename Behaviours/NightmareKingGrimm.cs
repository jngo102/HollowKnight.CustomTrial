using System.Collections;
using System.Linq;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    public class NightmareKingGrimm : MonoBehaviour
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
            _control.SetState("Init");
            
            yield return new WaitWhile(() => _control.ActiveStateName != "Dormant");
            
            _constrainX.Fsm.GetFsmFloat("Edge L").Value = ArenaInfo.LeftX;
            _constrainX.Fsm.GetFsmFloat("Edge R").Value = ArenaInfo.RightX;

            _constrainY.GetAction<FloatCompare>("Check").float2.Value = ArenaInfo.BottomY;
            _constrainY.GetAction<SetFloatValue>("Constrain").floatValue.Value = ArenaInfo.BottomY;

            _control.Fsm.GetFsmFloat("Min X").Value = ArenaInfo.LeftX;
            _control.Fsm.GetFsmFloat("Mid Y").Value = ArenaInfo.CenterY;
            _control.Fsm.GetFsmFloat("Max X").Value = ArenaInfo.RightX;
            _control.Fsm.GetFsmFloat("Ground Y").Value = ArenaInfo.BottomY + 2;
            _control.GetAction<FloatCompare>("Balloon Check").float2 = ArenaInfo.BottomY + 3;
            _control.GetAction<SetPosition>("Balloon Pos").x = ArenaInfo.CenterX;

            _control.GetState("HUD Canvas OUT").RemoveAction<SendEventByName>();

            _control.GetState("Death Start").InsertMethod(0, () =>
            {
                Destroy(FindObjectsOfType<GameObject>().First(go => go.name.Contains("Grimm Spike Holder")));
                ColosseumManager.EnemyCount--;
            });

            _control.SendEvent("TELE OUT");
        }
    }
}