using System.Collections;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    internal class WingedNosk : MonoBehaviour
    {
        private GameObject _globDropper;
        private GameObject _roofDust;

        private PlayMakerFSM _nosk;

        private void Awake()
        {
            _nosk = gameObject.LocateMyFSM("Hornet Nosk");

            _globDropper = Instantiate(CustomTrial.GameObjects["globdropper"]);
            _globDropper.SetActive(true);
            for (int globIndex = 1; globIndex < 9; globIndex++)
            {
                if (globIndex == 7) continue;
                _globDropper.transform.Find($"G{globIndex}").SetPosition2D(
                    ArenaInfo.LeftX + globIndex * (ArenaInfo.RightX - ArenaInfo.LeftX) / 9,
                    ArenaInfo.TopY - 1.5f
                );
            }

            _roofDust = Instantiate(CustomTrial.GameObjects["roofdust"]);
            _roofDust.transform.SetPosition2D(ArenaInfo.CenterX, ArenaInfo.TopY);

            GetComponent<HealthManager>().OnDeath += OnDeath;
        }

        private IEnumerator Start()
        {
            _nosk.Fsm.GetFsmFloat("X Max").Value = ArenaInfo.RightX;
            _nosk.Fsm.GetFsmFloat("X Min").Value = ArenaInfo.LeftX;
            _nosk.Fsm.GetFsmFloat("Y Max").Value = ArenaInfo.TopY;
            _nosk.Fsm.GetFsmFloat("Y Min").Value = ArenaInfo.BottomY;
            _nosk.Fsm.GetFsmFloat("Swoop Height").Value = ArenaInfo.CenterY;
            
            _nosk.GetAction<FloatCompare>("Swoop L").float2 = ArenaInfo.CenterX;
            _nosk.GetAction<FloatCompare>("Swoop R").float2 = ArenaInfo.CenterX;
            _nosk.GetAction<FloatCompare>("Shift Down?").float2 = ArenaInfo.CenterY;
            _nosk.GetAction<SetPosition>("Roof Impact").y = ArenaInfo.DefaultTopY + 2;
            _nosk.GetAction<SetPosition>("Roof Return").y = ArenaInfo.CenterY;
            
            _nosk.SetState("Init");

            yield return new WaitUntil(() => _nosk.ActiveStateName == "Dormant");

            _nosk.Fsm.GetFsmGameObject("Glob Dropper").Value = _globDropper;
            _nosk.Fsm.GetFsmGameObject("Roof Dust").Value = _roofDust;

            _nosk.SetState("Idle");
        }

        private void OnDeath()
        {
            Destroy(_globDropper);
            Destroy(_roofDust);
        }
    }
}