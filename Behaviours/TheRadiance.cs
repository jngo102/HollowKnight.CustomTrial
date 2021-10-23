using System.Collections;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    public class TheRadiance : MonoBehaviour
    {
        private PlayMakerFSM _commands;
        private PlayMakerFSM _control;

        private void Awake()
        {
            _commands = gameObject.LocateMyFSM("Attack Commands");
            _control = gameObject.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            _commands.Fsm.GetFsmFloat("Orb Max X").Value = ArenaInfo.RightX - 3;
            _commands.Fsm.GetFsmFloat("Orb Max Y").Value = ArenaInfo.TopY - 3;
            _commands.Fsm.GetFsmFloat("Orb Min X").Value = ArenaInfo.LeftX + 3;
            _commands.Fsm.GetFsmFloat("Orb Min Y").Value = ArenaInfo.BottomY + 3;
            
            _control.Fsm.GetFsmFloat("A1 X Max").Value = ArenaInfo.RightX;
            _control.Fsm.GetFsmFloat("A1 X Min").Value = ArenaInfo.LeftX;

            _control.GetAction<RandomFloat>("Set Dest", 4).min.Value = ArenaInfo.BottomY + 5;
            _control.GetAction<RandomFloat>("Set Dest", 4).max.Value = ArenaInfo.BottomY + 5.2f;

            _control.GetAction<SetFsmVector3>("First Tele").setValue.Value = new Vector3(ArenaInfo.CenterX, ArenaInfo.BottomY + 5, 0.006f);
            
            yield return null;
        }
    }
}