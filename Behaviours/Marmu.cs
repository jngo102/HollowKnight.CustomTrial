using HutongGames.PlayMaker.Actions;
using Modding;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    internal class Marmu : MonoBehaviour
    {
        private PlayMakerFSM _control;

        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");

            ReflectionHelper.GetField<EnemyDeathEffects, GameObject>(GetComponent<EnemyDeathEffectsNoEffect>(), "corpse").LocateMyFSM("Control").GetState("End").RemoveAction<CreateObject>();
        }

        private void Start()
        {
            _control.Fsm.GetFsmFloat("Tele X Max").Value = ArenaInfo.RightX - 3;
            _control.Fsm.GetFsmFloat("Tele X Min").Value = ArenaInfo.LeftX + 3;
            _control.Fsm.GetFsmFloat("Tele Y Max").Value = ArenaInfo.TopY - 3;
            _control.Fsm.GetFsmFloat("Tele Y Min").Value = ArenaInfo.BottomY + 3;
        }
    }
}