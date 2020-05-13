using CustomTrial.Behaviours;
using UnityEngine;

namespace CustomTrial
{
    public class EnemyTracker : MonoBehaviour
    {
        private HealthManager _hm;
        private PlayMakerFSM _fsm;

        private void Awake()
        {
            _hm = GetComponent<HealthManager>();
            _fsm = GetComponent<PlayMakerFSM>();
            _hm.OnDeath += OnDeath;
        }

        private void Start()
        {
            string goName = gameObject.name;

            if (goName.Contains("Infected Knight"))
            {
                gameObject.AddComponent<BrokenVessel>();
            }
            else if (goName.Contains("Mawlek Body"))
            {
                gameObject.AddComponent<BroodingMawlek>();
            }
            else if (goName.Contains("Jar Collector"))
            {
                gameObject.AddComponent<TheCollector>();
            }
            else if (goName.Contains("Mega Zombie Beam Miner"))
            {
                gameObject.AddComponent<CrystalGuardian>();
            }
            else if (goName.Contains("Zombie Beam Miner Rematch"))
            {
                gameObject.AddComponent<EnragedGuardian>();
            }
            else if (goName.Contains("Dung Defender"))
            {
                gameObject.AddComponent<DungDefender>();
            }
            else if (goName.Contains("False Knight Dream"))
            {
                gameObject.AddComponent<FailedChampion>();
            }
            else if (goName.Contains("False Knight New"))
            {
                gameObject.AddComponent<FalseKnight>();
            }
            else if (goName.Contains("Ghost Warrior Galien"))
            {
                gameObject.AddComponent<Galien>();
            }
            else if (goName.Contains("Ghost Warrior Slug"))
            {
                gameObject.AddComponent<Gorb>();
            }
            else if (goName.Contains("Ghost Warrior Hu"))
            {
                gameObject.AddComponent<ElderHu>();
            }
            else if (goName.Contains("Ghost Warrior Markoth"))
            {
                gameObject.AddComponent<Markoth>();
            }
            else if (goName.Contains("Ghost Warrior Marmu"))
            {
                gameObject.AddComponent<Marmu>();
            }
            else if (goName.Contains("Ghost Warrior No Eyes"))
            {
                gameObject.AddComponent<NoEyes>();
            }
            else if (goName.Contains("Ghost Warrior Xero"))
            {
                gameObject.AddComponent<Xero>();
            }
            else if (goName.Contains("Grey Prince"))
            {
                gameObject.AddComponent<GreyPrinceZote>();
            }
            else if (goName.Contains("Grimm Boss"))
            {
                gameObject.AddComponent<TroupeMasterGrimm>();
            }
            else if (goName.Contains("Hive Knight"))
            {
                gameObject.AddComponent<HiveKnight>();
            }
            else if (goName.Contains("HK Prime"))
            {
                gameObject.AddComponent<PureVessel>();
            }
            else if (goName.Contains("Hornet Boss 1"))
            {
                gameObject.AddComponent<HornetProtector>();
            }
            else if (goName.Contains("Mantis Traitor Lord"))
            {
                gameObject.AddComponent<TraitorLord>();
            }
            else if (goName.Contains("Mega Moss Charger"))
            {
                gameObject.AddComponent<MassiveMossCharger>();
            }
            else if (goName.Contains("Nightmare Grimm Boss"))
            {
                gameObject.AddComponent<NightmareKingGrimm>();
            }
            else if (goName.Contains("White Defender"))
            {
                gameObject.AddComponent<WhiteDefender>();
            }
            else
            {
                _fsm.SetState("Init");
            }
        }

        private void OnDeath()
        {
            ColosseumManager.EnemyCount--;
        }
    }
}