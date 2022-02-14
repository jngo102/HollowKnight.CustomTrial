using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Modding;
using System.Collections;
using System.Linq;
using UnityEngine;
using Vasi;
namespace CustomTrial.Behaviours
{
    internal class Galien : MonoBehaviour
    {
        private GameObject _hammer;

        private PlayMakerFSM _movement;

        private void Awake()
        {
            _movement = gameObject.LocateMyFSM("Movement");

            _hammer = Instantiate(CustomTrial.GameObjects["hammer"]);
            _hammer.transform.SetPosition2D(transform.position);
            _hammer.LocateMyFSM("Attack").Fsm.GetFsmGameObject("Ghost Warrior Galien").Value = gameObject;
            _hammer.AddComponent<GalienHammer>();
            _hammer.SetActive(true);

            var corpse = ReflectionHelper.GetField<EnemyDeathEffects, GameObject>(GetComponent<EnemyDeathEffectsNoEffect>(), "corpse");
            corpse.LocateMyFSM("Control").GetState("End").RemoveAction<CreateObject>();

            GetComponent<HealthManager>().OnDeath += OnDeath;
        }

        private void Start()
        {
            for (int index = 1; index <= 7; index++)
            {
                _movement.Fsm.GetFsmVector3($"P{index}").Value = RandomVector3();
            }

            FsmState broadcastDeathSet = gameObject.LocateMyFSM("Broadcast Ghost Death").GetState("Set");
            broadcastDeathSet.RemoveAction<SendEventByName>();
            broadcastDeathSet.AddMethod(() => {
                foreach (GameObject miniHammer in FindObjectsOfType<GameObject>().Where(obj => obj.name.Contains("Galien Mini Hammer")))
                {
                    FSMUtility.SendEventToGameObject(miniHammer, "GHOST DEATH");
                }
                FSMUtility.SendEventToGameObject(_hammer, "GHOST DEATH");
            });
        }

        private void OnDeath()
        {
            Destroy(_hammer);
        }

        private Vector3 RandomVector3()
        {
            float x = Random.Range(ArenaInfo.LeftX, ArenaInfo.RightX);
            float y = Random.Range(ArenaInfo.BottomY, ArenaInfo.TopY);
            float z = 0.006f;

            return new Vector3(x, y, z);
        }
    }

    internal class GalienHammer : MonoBehaviour
    {
        private PlayMakerFSM _attack;
        private PlayMakerFSM _control;

        private void Awake()
        {
            _attack = gameObject.LocateMyFSM("Attack");
            _control = gameObject.LocateMyFSM("Control");
        }

        private IEnumerator Start()
        {
            _attack.Fsm.GetFsmFloat("Floor Y").Value = ArenaInfo.BottomY;
            _attack.Fsm.GetFsmFloat("Slam Y").Value = ArenaInfo.BottomY + 0.4f;
            _attack.Fsm.GetFsmFloat("Wall L X").Value = ArenaInfo.LeftX;
            _attack.Fsm.GetFsmFloat("Wall R X").Value = ArenaInfo.RightX;

            _control.GetAction<SetVector3XYZ>("Init").x = transform.position.x;

            _attack.SetState(_attack.Fsm.StartState);
            _control.SetState(_control.Fsm.StartState);

            _control.SendEvent("READY");

            yield return new WaitUntil(() => _control.ActiveStateName == "Emerge");

            _control.SendEvent("READY");
        }
    }
}