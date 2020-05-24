using System.Collections;
using CustomTrial.Utilities;
using HutongGames.PlayMaker.Actions;
using ModCommon;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class GreatNailsageSly : MonoBehaviour
    {
        private PlayMakerFSM _control;
        
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            gameObject.PrintSceneHierarchyTree();
            
            _control.SetState("Init");

            _control.Fsm.GetFsmBool("Final Rage").Value = true;
            
            _control.GetAction<FloatCompare>("Cyc Down").float2.Value = ArenaInfo.BottomY + 4;
            _control.GetAction<FloatOperator>("Cyc Jump Launch").float1.Value = ArenaInfo.CenterX;
            _control.GetAction<SetFloatValue>("Jump To L", 0).floatValue.Value = ArenaInfo.RightX - 8;
            _control.GetAction<SetFloatValue>("Jump To L", 1).floatValue.Value = ArenaInfo.LeftX;
            _control.GetAction<SetFloatValue>("Jump To R", 0).floatValue.Value = ArenaInfo.LeftX + 8;
            _control.GetAction<SetFloatValue>("Jump To R", 1).floatValue.Value = ArenaInfo.RightX;
            
            _control.InsertMethod("Bow", 0, () => Destroy(gameObject, 3));
            _control.InsertMethod("Stun Wait", _control.GetState("Stun Wait").Actions.Length, () => _control.SendEvent("READY"));
            _control.InsertMethod("Grabbing", _control.GetState("Grabbing").Actions.Length, () => _control.SendEvent("GRABBED"));
            
            _control.RemoveAction<SetPolygonCollider>("Cyclone Start");
            _control.RemoveAction("Cyclone Start", 8);
            _control.RemoveAction<SetPolygonCollider>("Cyclone End");
            _control.RemoveAction<SetPolygonCollider>("Stun Reset");
            _control.RemoveAction<SetPolygonCollider>("Death Reset");
            
            yield return new WaitWhile(() => _control.ActiveStateName != "Docile");

            GameObject spinTink = new GameObject("Spin Tink");
            var collider = spinTink.AddComponent<CircleCollider2D>();
            spinTink.AddComponent<DamageHero>();
            spinTink.AddComponent<DebugColliders>();
            collider.isTrigger = true;
            collider.radius = 3;
            spinTink.transform.SetParent(gameObject.transform, false);
            _control.Fsm.GetFsmGameObject("Spin Tink").Value = spinTink;

            _control.SetState("Battle Start");
        }

        private void Update()
        {
            Modding.Logger.Log("[Sly] " + _control.ActiveStateName);
        }
    }
}