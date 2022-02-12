using HutongGames.PlayMaker.Actions;
using Modding;
using System.Collections;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    public class DungDefender : MonoBehaviour
    {
        private PlayMakerFSM _dd;

        private void Awake()
        {
            _dd = gameObject.LocateMyFSM("Dung Defender");
        }

        private IEnumerator Start()
        {
            _dd.SetState("Init");

            _dd.Fsm.GetFsmFloat("Centre X").Value = ArenaInfo.CenterX;
            _dd.Fsm.GetFsmFloat("Dolphin Max X").Value = ArenaInfo.RightX - 4;
            _dd.Fsm.GetFsmFloat("Dolphin Min X").Value = ArenaInfo.LeftX + 4;
            _dd.Fsm.GetFsmFloat("Erupt Y").Value = ArenaInfo.BottomY;
            _dd.Fsm.GetFsmFloat("Max X").Value = ArenaInfo.RightX;
            _dd.Fsm.GetFsmFloat("Min X").Value = ArenaInfo.LeftX;

            GameObject burrowEffect = _dd.Fsm.GetFsmGameObject("Burrow Effect").Value;
            burrowEffect.transform.position += Vector3.down * 6;
            burrowEffect.LocateMyFSM("Burrow Effect").Fsm.GetFsmFloat("Ground Y").Value -= 6;

            _dd.GetState("Roar?").RemoveAction<SendEventByName>();
            _dd.GetState("Roar?").RemoveAction<SetFsmGameObject>();
            _dd.GetState("Rage Roar").RemoveAction<SendEventByName>();
            _dd.GetState("Rage Roar").RemoveAction<SetFsmGameObject>();
            _dd.GetState("Init").RemoveAction<GetPlayerDataInt>();

            GameObject corpse = ReflectionHelper.GetField<EnemyDeathEffects, GameObject>(GetComponent<EnemyDeathEffectsUninfected>(), "corpse");
            corpse.LocateMyFSM("Corpse").GetState("Blow").AddMethod(() => Destroy(corpse));

            yield return new WaitUntil(() => _dd.ActiveStateName == "Sleep");

            GetComponent<HealthManager>().IsInvincible = false;
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Rigidbody2D>().gravityScale = _dd.Fsm.GetFsmFloat("Gravity").Value;

            _dd.SetState("Idle");
        }
    }
}