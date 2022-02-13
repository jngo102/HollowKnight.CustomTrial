using HutongGames.PlayMaker.Actions;
using System.Collections.Generic;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    public class CrystalGuardian : MonoBehaviour
    {
        private List<GameObject> _turrets = new();

        private PlayMakerFSM _miner;
        
        private void Awake()
        {
            _miner = gameObject.LocateMyFSM("Beam Miner");

            for (float x = ArenaInfo.LeftX; x <= ArenaInfo.RightX; x += Random.Range(5, 7))
            {
                GameObject turret = Instantiate(CustomTrial.GameObjects["turretcg1"]);
                turret.transform.SetPosition2D(x, ArenaInfo.TopY);
                turret.SetActive(true);
                _turrets.Add(turret);
            }

            GetComponent<HealthManager>().OnDeath += OnDeath;
        }

        private void Start()
        {
            Destroy(transform.Find("Cam Lock").gameObject);

            _miner.GetAction<FaceObject>("Face Hero").objectB = HeroController.instance.gameObject;

            _miner.Fsm.GetFsmFloat("Jump Max X").Value = ArenaInfo.LeftX;
            _miner.Fsm.GetFsmFloat("Jump Min X").Value = ArenaInfo.RightX;

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