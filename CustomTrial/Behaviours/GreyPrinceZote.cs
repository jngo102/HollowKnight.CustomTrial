using System.Collections;
using HutongGames.PlayMaker;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    public class GreyPrinceZote : MonoBehaviour
    {
        private PlayMakerFSM _control;

        private void Awake()
        {
            Destroy(gameObject.LocateMyFSM("Constrain X"));
            _control = gameObject.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            _control.Fsm.GetFsmFloat("Left X").Value = ArenaInfo.LeftX + 2;
            _control.Fsm.GetFsmFloat("Right X").Value = ArenaInfo.RightX - 2;

            _control.GetAction<GGCheckIfBossScene>("Level Check").regularSceneEvent = new FsmEvent("3");

            _control.GetAction<SetDamageHeroAmount>("Set Damage", 0).damageDealt = 1;
            _control.GetAction<SetDamageHeroAmount>("Set Damage", 1).damageDealt = 1;
            _control.GetAction<SetDamageHeroAmount>("Set Damage", 2).damageDealt = 1;

            _control.SetState("Pause");

            yield return new WaitUntil(() => _control.ActiveStateName == "Dormant");

            _control.SetState("Activate");
        }
    }
}