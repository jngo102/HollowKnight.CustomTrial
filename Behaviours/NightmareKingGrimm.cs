using HutongGames.PlayMaker.Actions;
using Modding;
using System.Collections;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    internal class NightmareKingGrimm : MonoBehaviour
    {
        private GameObject _grimmBats;
        private GameObject _spikeHolder;

        private PlayMakerFSM _constrainX;
        private PlayMakerFSM _constrainY;
        private PlayMakerFSM _control;
        
        private void Awake()
        {
            _constrainX = gameObject.LocateMyFSM("constrain_x");
            _constrainY = gameObject.LocateMyFSM("Constrain Y");
            _control = gameObject.LocateMyFSM("Control");

            _spikeHolder = Instantiate(CustomTrial.GameObjects["nightmaregrimmspikeholder"]);
            _spikeHolder.transform.SetPosition2D(ArenaInfo.LeftX, ArenaInfo.BottomY - 3);
            _spikeHolder.SetActive(true);

            _grimmBats = Instantiate(CustomTrial.GameObjects["nightmaregrimmbats"]);
            _grimmBats.transform.SetPosition2D(ArenaInfo.CenterX, ArenaInfo.CenterY);
            _grimmBats.SetActive(true);
        }

        private IEnumerator Start()
        {
            PlayMakerFSM batCtrl = _grimmBats.transform.Find("Real Bat").gameObject.LocateMyFSM("Control");
            batCtrl.Fsm.GetFsmGameObject("Grimm").Value = gameObject;
            batCtrl.GetAction<FloatCompare>("Face Middle").float2 = ArenaInfo.CenterX;
            batCtrl.GetAction<iTweenMoveTo>("Get To Middle").vectorPosition = new Vector3(ArenaInfo.CenterX, ArenaInfo.CenterY, 0);
            batCtrl.GetAction<FloatCompare>("Fly", 3).float2 = ArenaInfo.CenterX - 2;
            batCtrl.GetAction<FloatCompare>("Fly", 4).float2 = ArenaInfo.CenterX + 2;
            batCtrl.GetAction<FloatCompare>("Fly", 5).float2 = ArenaInfo.CenterY - 1;
            batCtrl.GetAction<FloatCompare>("Fly", 6).float2 = ArenaInfo.CenterY + 1;
            batCtrl.SetState(batCtrl.Fsm.StartState);
            batCtrl.SendEvent("BOSS AWAKE");

            foreach (var fakeBat in _grimmBats.transform.GetComponentsInChildren<FakeBat>(true))
            {
                ReflectionHelper.SetField(fakeBat, "grimm", transform);
            }

            _control.SetState("Init");
            
            yield return new WaitWhile(() => _control.ActiveStateName != "Dormant");
            
            _constrainX.Fsm.GetFsmFloat("Edge L").Value = ArenaInfo.LeftX;
            _constrainX.Fsm.GetFsmFloat("Edge R").Value = ArenaInfo.RightX;

            _constrainY.GetAction<FloatCompare>("Check").float2.Value = ArenaInfo.BottomY;
            _constrainY.GetAction<SetFloatValue>("Constrain").floatValue.Value = ArenaInfo.BottomY;

            _control.Fsm.GetFsmFloat("Min X").Value = ArenaInfo.LeftX;
            _control.Fsm.GetFsmFloat("Mid Y").Value = ArenaInfo.CenterY;
            _control.Fsm.GetFsmFloat("Max X").Value = ArenaInfo.RightX;
            _control.Fsm.GetFsmFloat("Ground Y").Value = ArenaInfo.BottomY + 2;
            _control.GetAction<FloatCompare>("Balloon Check").float2 = ArenaInfo.BottomY + 3;
            _control.GetAction<SetPosition>("Balloon Pos").x = ArenaInfo.CenterX;

            _control.GetState("Explode").RemoveAction(3);
            _control.GetState("Explode").RemoveAction(4);
            _control.GetState("HUD Canvas OUT").RemoveAction<SendEventByName>();
            _control.GetState("Spike Attack").RemoveAction<SendEventToRegister>();

            _control.GetState("Death Start").InsertMethod(0, () =>
            {
                Destroy(_grimmBats);
                Destroy(_spikeHolder);
                ColosseumManager.EnemyCount--;
            });
            _control.GetState("Explode").AddMethod(() => batCtrl.SendEvent("BATS OUT"));
            _control.GetState("Spike Attack").AddMethod(() => FSMUtility.SendEventToGameObject(_spikeHolder, "SPIKE ATTACK"));

            _control.SendEvent("TELE OUT");

            var spikeReady = _spikeHolder.LocateMyFSM("Spike Control").GetAction<RandomFloatEither>("Ready");
            spikeReady.value1 = spikeReady.value2 = ArenaInfo.LeftX;
        }
    }
}