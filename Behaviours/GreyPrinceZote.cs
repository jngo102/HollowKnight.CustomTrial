using System.Collections;
using System.Linq;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    internal class GreyPrinceZote : MonoBehaviour
    {
        private PlayMakerFSM _constrainX;
        private PlayMakerFSM _control;

        private void Awake()
        {
            _constrainX = gameObject.LocateMyFSM("Constrain X");
            _control = gameObject.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            _constrainX.Fsm.GetFsmFloat("Edge L").Value = ArenaInfo.LeftX;
            _constrainX.Fsm.GetFsmFloat("Edge R").Value = ArenaInfo.RightX;
            
            _control.Fsm.GetFsmFloat("Left X").Value = ArenaInfo.LeftX + 2;
            _control.Fsm.GetFsmFloat("Right X").Value = ArenaInfo.RightX - 2;

            _control.GetAction<GGCheckIfBossScene>("Level Check").regularSceneEvent = _control.Fsm.Events.First(@event => @event.Name == "3");

            _control.GetAction<SetDamageHeroAmount>("Set Damage", 0).damageDealt = 1;
            _control.GetAction<SetDamageHeroAmount>("Set Damage", 1).damageDealt = 1;
            _control.GetAction<SetDamageHeroAmount>("Set Damage", 2).damageDealt = 1;

            _control.SetState("Pause");

            yield return new WaitUntil(() => _control.ActiveStateName == "Dormant");

            GetComponent<HealthManager>().IsInvincible = false;
            _control.SetState("Activate");
        }
    }
}