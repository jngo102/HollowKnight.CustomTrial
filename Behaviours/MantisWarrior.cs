using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    public class MantisWarrior : MonoBehaviour
    {
        private PlayMakerFSM _mantis;

        private void Awake()
        {
            _mantis = gameObject.LocateMyFSM("Mantis");
        }

        private void Start()
        {
            _mantis.GetState("Lords Defeated?").GetAction<PlayerDataBoolTest>().isTrue = FsmEvent.FindEvent("FALSE");

            float sign = Mathf.Sign(HeroController.instance.transform.position.x - transform.position.x);
            transform.SetScaleX(sign);

            _mantis.Fsm.GetFsmFloat("Attack Speed Crt").Value = 30 * sign;
            _mantis.Fsm.GetFsmVector2("Raycast Vector").Value = Vector2.right * 2 * sign;
        }
    }
}