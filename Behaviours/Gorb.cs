using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Modding;
using System.Linq;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    internal class Gorb : MonoBehaviour
    {
        private PlayMakerFSM _movement;
        
        private void Awake()
        {
            _movement = gameObject.LocateMyFSM("Movement");
            Destroy(gameObject.LocateMyFSM("Distance Attack"));

            var corpse = ReflectionHelper.GetField<EnemyDeathEffects, GameObject>(GetComponent<EnemyDeathEffectsNoEffect>(), "corpse");
            corpse.LocateMyFSM("Control").GetState("End").RemoveAction<CreateObject>();
        }

        private void Start()
        {
            Destroy(gameObject.LocateMyFSM("Broadcast Ghost Death"));

            _movement.SetState(_movement.Fsm.StartState);

            for (int index = 1; index <= 7; index++)
            {
                _movement.Fsm.GetFsmVector3($"P{index}").Value = RandomVector3();
            }

            _movement.GetAction<FloatCompare>("Hover", 4).float2 = ArenaInfo.LeftX;
            _movement.GetAction<FloatCompare>("Hover", 5).float2 = ArenaInfo.RightX;
            _movement.GetAction<FloatCompare>("Hover", 6).float2 = ArenaInfo.CenterY;
            _movement.GetAction<FaceObject>("Hover").objectB = HeroController.instance.gameObject;
            
            _movement.GetAction<FloatTestToBool>("Set Warp", 2).float2 = ArenaInfo.CenterX;
            _movement.GetAction<FloatTestToBool>("Set Warp", 3).float2 = ArenaInfo.CenterX;

            _movement.GetAction<SetPosition>("Return").x = ArenaInfo.CenterX;
            _movement.GetAction<SetPosition>("Return").y = ArenaInfo.CenterY;
        }

        private Vector3 RandomVector3()
        {
            float x = Random.Range(ArenaInfo.LeftX, ArenaInfo.RightX);
            float y = Random.Range(ArenaInfo.BottomY, ArenaInfo.TopY);

            return new Vector3(x, y, .006f);
        }
    }
}