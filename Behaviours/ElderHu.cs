using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Modding;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    internal class ElderHu : MonoBehaviour
    {
        private GameObject _ringHolder;

        private PlayMakerFSM _attacking;
        private PlayMakerFSM _movement;

        private void Awake()
        {
            _attacking = gameObject.LocateMyFSM("Attacking");
            _movement = gameObject.LocateMyFSM("Movement");

            _ringHolder = Instantiate(CustomTrial.GameObjects["ringholder"]);
            _ringHolder.SetActive(true);
            _ringHolder.name = _ringHolder.name.Replace("(Clone)", "");

            var corpse = ReflectionHelper.GetField<EnemyDeathEffects, GameObject>(GetComponent<EnemyDeathEffectsNoEffect>(), "corpse");
            corpse.LocateMyFSM("Control").GetState("End").RemoveAction<CreateObject>();

            GetComponent<HealthManager>().OnDeath += OnDeath;
        }

        private void Start()
        {
            _attacking.SetState(_attacking.Fsm.StartState);

            _attacking.GetAction<FloatCompare>("Choose Pos", 1).float2 = ArenaInfo.CenterX - 6;
            _attacking.GetAction<FloatCompare>("Choose Pos", 2).float2 = ArenaInfo.CenterX + 6;
            _attacking.GetAction<SetPosition>("Set L").x = ArenaInfo.LeftX + 2;
            _attacking.GetAction<SetPosition>("Set L").y = transform.position.y;
            _attacking.GetAction<SetPosition>("Set R").x = ArenaInfo.RightX - 2;
            _attacking.GetAction<SetPosition>("Set R").y = transform.position.y;

            _attacking.GetAction<SetPosition>("Mega Warp Out").x = HeroController.instance.transform.position.x;

            _attacking.SendEvent("READY");

            for (int index = 1; index <= 7; index++)
            {
                _movement.Fsm.GetFsmVector3($"P{index}").Value = RandomVector3();
            }
            
            _movement.GetAction<FloatCompare>("Choose L").float2 = ArenaInfo.CenterX - 10;
            _movement.GetAction<FloatCompare>("Choose R").float2 = ArenaInfo.CenterX + 10;
            _movement.GetAction<FloatCompare>("Set Warp").float2 = ArenaInfo.CenterX;
            _movement.GetAction<SetVector3XYZ>("Choose L").x = ArenaInfo.CenterX - 5;
            _movement.GetAction<SetVector3XYZ>("Choose L").y = transform.position.y;
            _movement.GetAction<SetVector3XYZ>("Choose R").x = ArenaInfo.CenterX + 5;
            _movement.GetAction<SetVector3XYZ>("Choose R").y = transform.position.y;
            _movement.GetAction<SetPosition>("Return").x = ArenaInfo.CenterX;
            _movement.GetAction<SetPosition>("Return").y = ArenaInfo.CenterY;

            foreach (Transform ringTransform in _ringHolder.transform)
            {
                ringTransform.position = new Vector2(ringTransform.position.x, HeroController.instance.transform.position.y + 3);
                PlayMakerFSM ringCtrl = ringTransform.GetComponent<PlayMakerFSM>();
                ringCtrl.GetAction<FloatCompare>("Down").float2 = ArenaInfo.BottomY + 1;
                FsmState checkPos = ringCtrl.GetState("Check Pos");
                checkPos.GetAction<FloatCompare>(1).lessThan = new FsmEvent("RESET");
                checkPos.GetAction<FloatCompare>(2).greaterThan = new FsmEvent("RESET");
                checkPos.RemoveTransition("CANCEL");
                checkPos.AddTransition("RESET", "Reset");
                //ringCtrl.GetState("Reset").RemoveAction<SetPosition>();
                ringCtrl.GetState("Antic").InsertMethod(0, () => ringTransform.position = new Vector2(ringTransform.position.x, HeroController.instance.transform.position.y + 3));
                ringCtrl.SetState(ringCtrl.Fsm.StartState);
            }
        }

        private void OnDeath()
        {
            Destroy(_ringHolder);
        }

        private Vector3 RandomVector3()
        {
            float x = Random.Range(ArenaInfo.LeftX, ArenaInfo.RightX);
            float y = Random.Range(ArenaInfo.BottomY, ArenaInfo.TopY);
            float z = 0.006f;

            return new Vector3(x, y, z);
        }
    }
}