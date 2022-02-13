using HutongGames.PlayMaker.Actions;
using System.Collections.Generic;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    internal class EnragedGuardian : MonoBehaviour
    {
        private List<GameObject> _turrets = new();

        private PlayMakerFSM _miner;
        
        private void Awake()
        {
            _miner = gameObject.LocateMyFSM("Beam Miner");

            for (float x = ArenaInfo.LeftX; x <= ArenaInfo.RightX; x += Random.Range(5, 7))
            {
                GameObject turret = Instantiate(CustomTrial.GameObjects["turretcg2"]);
                turret.transform.SetPosition2D(x, ArenaInfo.TopY);
                turret.SetActive(true);
                _turrets.Add(turret);
            }

            GetComponent<HealthManager>().OnDeath += OnDeath;
        }

        private void Start()
        {
            _miner.GetAction<FaceObject>("Face Hero 2").objectB = HeroController.instance.gameObject;

            _miner.GetState("Laser Shoot").RemoveAction<SendEventByName>();

            _miner.GetState("Laser Shoot").AddMethod(() =>
            {
                foreach (GameObject turrent in _turrets)
                {
                    turrent.GetComponent<PlayMakerFSM>().SendEvent("LASER SHOOT");
                }
            });

            _miner.Fsm.GetFsmFloat("Jump Max X").Value = ArenaInfo.RightX;
            _miner.Fsm.GetFsmFloat("Jump Min X").Value = ArenaInfo.LeftX;

            _miner.SetState("Init");
        }

        private void OnDeath()
        {
            foreach (GameObject turret in _turrets)
            {
                Destroy(turret);
            }
        }
    }
}