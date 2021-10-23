using HutongGames.PlayMaker.Actions;
using System.Collections;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    public class TheCollector : MonoBehaviour
    {
        private PlayMakerFSM _control;
        private PlayMakerFSM _death;
        
        private void Awake()
        {
            _control = gameObject.LocateMyFSM("Control");
            _death = gameObject.LocateMyFSM("Death");
        }

        private IEnumerator Start()
        {
            _control.Fsm.GetFsmFloat("Bottle XL").Value = ArenaInfo.LeftX + 2;
            _control.Fsm.GetFsmFloat("Bottle XR").Value = ArenaInfo.RightX - 2;
            _control.Fsm.GetFsmFloat("Roof X L").Value = ArenaInfo.LeftX + 2;
            _control.Fsm.GetFsmFloat("Roof X R").Value = ArenaInfo.RightX - 2;
            _control.Fsm.GetFsmFloat("Roof Y").Value = ArenaInfo.TopY - 2;
            _control.Fsm.GetFsmFloat("Return Y").Value = ArenaInfo.TopY - 2;

            GameObject spawnJar = _control.GetAction<SpawnObjectFromGlobalPool>("Spawn").gameObject.Value;
            spawnJar.GetComponent<SpawnJarControl>().spawnY = ArenaInfo.TopY - 5;
            spawnJar.GetComponent<SpawnJarControl>().breakY = ArenaInfo.BottomY;
            spawnJar.CreatePool(3);
            _control.GetAction<SpawnObjectFromGlobalPool>("Spawn").gameObject.Value = spawnJar;
            _control.Fsm.GetFsmGameObject("Spawn Jar").Value = spawnJar;
            _control.GetAction<FloatCompare>("Spawn").tolerance = 1;

            _death.ChangeTransition("Start Effect", "FINISHED", "Kill Enemies");
            _death.GetState("Send Event").AddMethod(() => ColosseumManager.EnemyCount--);

            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;

            _control.SetState("Init");

            yield return new WaitUntil(() => _control.ActiveStateName == "Sleep");

            _control.SetState("Roar Recover");
        }
    }
}