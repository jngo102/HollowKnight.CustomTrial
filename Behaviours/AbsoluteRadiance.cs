using System.Collections;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    class AbsoluteRadiance : MonoBehaviour
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
            _commands.Fsm.GetFsmFloat("Orb Max X").Value = ArenaInfo.RightX - 1;
            _commands.Fsm.GetFsmFloat("Orb Max Y").Value = ArenaInfo.TopY - 1;
            _commands.Fsm.GetFsmFloat("Orb Min X").Value = ArenaInfo.LeftX + 1;
            _commands.Fsm.GetFsmFloat("Orb Min Y").Value = ArenaInfo.BottomY + 3;

            GameObject comb = _commands.GetAction<SpawnObjectFromGlobalPool>("Comb Top").gameObject.Value;
            comb.transform.position = new Vector3(ArenaInfo.CenterX, ArenaInfo.CenterY, 0.006f);

            PlayMakerFSM combControl = comb.LocateMyFSM("Control");
            combControl.GetAction<SetPosition>("TL").x = ArenaInfo.LeftX;
            combControl.GetAction<SetPosition>("TR").x = ArenaInfo.RightX;
            combControl.GetAction<RandomFloat>("Top").min = ArenaInfo.CenterX - 1;
            combControl.GetAction<RandomFloat>("Top").max = ArenaInfo.CenterX + 1;
            combControl.GetAction<SetPosition>("Top").y = ArenaInfo.TopY;
            combControl.GetAction<SetPosition>("L").x = ArenaInfo.LeftX;
            combControl.GetAction<SetPosition>("L").y = ArenaInfo.CenterY;
            combControl.GetAction<SetPosition>("R").x = ArenaInfo.RightX;
            combControl.GetAction<SetPosition>("R").y = ArenaInfo.CenterY;
            
            _commands.GetAction<SpawnObjectFromGlobalPool>("Comb Top").gameObject = comb;
            _commands.GetAction<SpawnObjectFromGlobalPool>("Comb L").gameObject = comb;
            _commands.GetAction<SpawnObjectFromGlobalPool>("Comb R").gameObject = comb;

            _control.Fsm.GetFsmFloat("A1 X Max").Value = ArenaInfo.RightX - 2;
            _control.Fsm.GetFsmFloat("A1 X Min").Value = ArenaInfo.LeftX + 2;

            _control.GetAction<RandomFloat>("Set Dest", 4).min = transform.position.y - 1;
            _control.GetAction<RandomFloat>("Set Dest", 4).max = transform.position.y + 1;
            _control.GetAction<RandomFloat>("Set Dest 2", 4).min = transform.position.y - 1;
            _control.GetAction<RandomFloat>("Set Dest 2", 4).max = transform.position.y + 1;
            _control.GetAction<SetFsmVector3>("First Tele").setValue = transform.position;
            _control.GetAction<SetFsmVector3>("Rage1 Tele").setValue = transform.position;
            _control.GetState("Climb Plats").InsertMethod(0, () =>
            {
                ColosseumManager.EnemyCount--;
                Destroy(gameObject);
            });

            _commands.SetState("Init");
            _control.SetState("Init");

            yield return new WaitUntil(() => _control.ActiveStateName == "Intro End");

            _control.SetState("Arena 1 Idle");
        }
    }
}
