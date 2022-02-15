using HutongGames.PlayMaker.Actions;
using Modding;
using System.Collections;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    internal class TroupeMasterGrimm : MonoBehaviour
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

            _spikeHolder = Instantiate(CustomTrial.GameObjects["grimmspikeholder"]);
            _spikeHolder.transform.SetPosition2D(ArenaInfo.LeftX, ArenaInfo.BottomY - 3);
            _spikeHolder.SetActive(true);

            _grimmBats = Instantiate(CustomTrial.GameObjects["grimmbats"]);
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

            _constrainX.Fsm.GetFsmFloat("Edge L").Value = ArenaInfo.LeftX + 1;
            _constrainX.Fsm.GetFsmFloat("Edge R").Value = ArenaInfo.RightX - 1;

            _constrainY.GetAction<FloatCompare>("Check").float2.Value = ArenaInfo.BottomY;
            _constrainY.GetAction<SetFloatValue>("Constrain").floatValue.Value = ArenaInfo.BottomY;

            _control.Fsm.GetFsmFloat("AD Max X").Value = ArenaInfo.RightX - 1;
            _control.Fsm.GetFsmFloat("AD Min X").Value = ArenaInfo.LeftX + 5;
            _control.Fsm.GetFsmFloat("Ground Y").Value = ArenaInfo.BottomY + 2;
            _control.Fsm.GetFsmFloat("Max X").Value = ArenaInfo.RightX;
            _control.Fsm.GetFsmFloat("Min X").Value = ArenaInfo.LeftX;

            _control.GetAction<SetPosition>("Balloon Pos").x = ArenaInfo.CenterX;
            _control.GetAction<SetPosition>("Balloon Pos").y = 14.0f;

            _control.GetState("Death Start").InsertMethod(0, () =>
            {
                Destroy(_grimmBats);
                Destroy(_spikeHolder);
                ColosseumManager.EnemyCount--;
            });

            _control.GetState("Bow").RemoveAction<ApplyMusicCue>();
            _control.GetState("Bow").RemoveAction<TransitionToAudioSnapshot>();
            _control.GetState("Explode").RemoveAction(3);
            _control.GetState("Explode").RemoveAction(3);
            _control.GetState("Spike Attack").RemoveAction<SendEventToRegister>();

            _control.GetState("Explode").AddMethod(() => batCtrl.SendEvent("BATS OUT"));
            _control.GetState("Spike Attack").AddMethod(() => FSMUtility.SendEventToGameObject(_spikeHolder, "SPIKE ATTACK"));

            _control.SetState("Init");

            var spikeReady = _spikeHolder.LocateMyFSM("Spike Control").GetAction<RandomFloatEither>("Ready");
            spikeReady.value1 = spikeReady.value2 = ArenaInfo.LeftX;

            yield return new WaitUntil(() => _control.ActiveStateName == "Bow");

            _control.SetState("Tele Out");
        }
    }
}