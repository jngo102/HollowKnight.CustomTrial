using System.Collections;
using HutongGames.PlayMaker.Actions;
using Modding;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    internal class PureVessel : MonoBehaviour
    {
        private const float GroundY = 6.4f;
        
        private PlayMakerFSM _control;
        
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            _control.SetState("Pause");
            
            var constrainPos = GetComponent<ConstrainPosition>();
            constrainPos.constrainX = constrainPos.constrainY = false;
            
            _control.Fsm.GetFsmFloat("Left X").Value = ArenaInfo.LeftX;
            _control.Fsm.GetFsmFloat("Right X").Value = ArenaInfo.RightX;
            _control.Fsm.GetFsmFloat("TeleRange Min").Value = ArenaInfo.LeftX + 2;
            _control.Fsm.GetFsmFloat("TeleRange Max").Value = ArenaInfo.RightX - 2;
            _control.Fsm.GetFsmFloat("Plume Y").Value = GroundY + 1;
            _control.Fsm.GetFsmFloat("Stun Land Y").Value = GroundY + 3;

            _control.GetAction<FloatCompare>("Pos Check", 2).float2.Value = ArenaInfo.LeftX + 10;
            _control.GetAction<FloatCompare>("Pos Check", 3).float2.Value = ArenaInfo.RightX - 10;

            _control.GetState("HUD Out").RemoveAction<SendEventByName>();

            _control.GetState("Stun Air").InsertMethod(0, () => _control.SetState("Stun Land"));

            GameObject corpsePrefab =
                ReflectionHelper.GetField<EnemyDeathEffects, GameObject>(GetComponent<EnemyDeathEffectsUninfected>(),
                    "corpse");
            corpsePrefab.LocateMyFSM("corpse").GetState("Music").RemoveAction<SendEventByName>();

            yield return new WaitWhile(() => _control.ActiveStateName != "Intro 1");

            _control.SetState("Intro Roar End");
        }
    }
}