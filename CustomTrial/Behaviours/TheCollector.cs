using System.Collections;
using CustomTrial.Utilities;
using HutongGames.PlayMaker.Actions;
using ModCommon;
using UnityEngine;

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
            _control.SetState("Init");

            _control.Fsm.GetFsmFloat("Bottle XL").Value = ArenaInfo.LeftX;
            _control.Fsm.GetFsmFloat("Bottle XR").Value = ArenaInfo.RightX;
            _control.Fsm.GetFsmFloat("Roof X L").Value = ArenaInfo.LeftX + 2;
            _control.Fsm.GetFsmFloat("Roof X R").Value = ArenaInfo.RightX - 2;
            _control.Fsm.GetFsmFloat("Roof Y").Value = ArenaInfo.TopY + 2;
            _control.Fsm.GetFsmFloat("Return Y").Value = ArenaInfo.TopY + 2;

            GameObject spawnJar = _control.GetAction<SpawnObjectFromGlobalPool>("Spawn").gameObject.Value;
            spawnJar.GetComponent<SpawnJarControl>().spawnY = ArenaInfo.TopY;
            spawnJar.GetComponent<SpawnJarControl>().breakY = ArenaInfo.BottomY;
            _control.GetAction<SpawnObjectFromGlobalPool>("Spawn").gameObject.Value = spawnJar;

            _control.GetAction<SetPosition>("Spawn").y.Value = ArenaInfo.TopY - 5;
            
            //_control.GetAction<SetPosition>("Spawn").y.Value = ArenaInfo.TopY - 2;

            _death.ChangeTransition("Start Effect", "FINISHED", "Kill Enemies");
            
            _control.InsertMethod("Start Land", _control.GetState("Start Land").Actions.Length, () => _control.SetState("Roar Recover"));

            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;

            _control.SetState("Roar Recover");

            yield return null;
        }

        private void Update()
        {
            Vector3 pos = _control.Fsm.GetFsmGameObject("Spawn Jar").Value.transform.position;
            if (pos.y >= ArenaInfo.TopY - 5)
            {
                _control.Fsm.GetFsmGameObject("Spawn Jar").Value.transform.position = new Vector3(pos.x, ArenaInfo.TopY - 5, pos.z);
            }
        }
    }
}