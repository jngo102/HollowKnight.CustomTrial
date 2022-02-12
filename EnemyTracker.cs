using System.Linq;
using CustomTrial.Behaviours;
using HutongGames.PlayMaker;
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
            _hm.OnDeath += () => ColosseumManager.EnemyCount--;
        }

        private void Start()
        {
            string goName = gameObject.name;

            if (goName.Contains("Hatcher"))
            {
                var hatcherCage = Instantiate(CustomTrial.GameObjects["aspidhatchling"]);
                hatcherCage.SetActive(true);
            }
            else if (goName.Contains("Mantis Flyer Child"))
            {
                gameObject.AddComponent<MantisYouth>();
            }
            else if (goName.Contains("Mantis") && !goName.Contains("Traitor") && !goName.Contains("Lord"))
            {
                gameObject.AddComponent<MantisWarrior>();
            }
            else if (goName.Contains("Mage Balloon"))
            {
                gameObject.AddComponent<Folly>();
            }
            else if (goName.Contains("Mage Blob"))
            {
                gameObject.AddComponent<Mistake>();
            }
            else if (goName.Contains("Plant Trap"))
            {
                gameObject.LocateMyFSM("Plant Trap Control").SetState("Init");
            }
            else if (goName.Contains("Baby Centipede"))
            {
                gameObject.AddComponent<Dirtcarver>();
            }
            else if (goName.Contains("Shade Sibling"))
            {
                gameObject.AddComponent<Sibling>();
            }
            else if (goName.Contains("Spider Flyer"))
            {
                gameObject.AddComponent<LittleWeaver>();
            }
            else if (goName.Contains("Parasite Balloon"))
            {
                gameObject.AddComponent<InfectedBalloon>();
            }
            else if (goName.Contains("Mage") && !goName.Contains("Knight") && !goName.Contains("Lord") && !goName.Contains("Electric"))
            {
                gameObject.AddComponent<SoulTwister>();
            }
            else if (goName.Contains("Electric Mage"))
            {
                gameObject.AddComponent<VoltTwister>();
            }
            else if (goName.Contains("Moss Knight") && !goName.Contains("Fat"))
            {
                gameObject.AddComponent<MossKnight>();
            }
            else if (goName.Contains("Moss Knight Fat"))
            {
                Destroy(gameObject.LocateMyFSM("FSM"));
            }
            else if (goName.Contains("Bee Hatchling Ambient"))
            {
                gameObject.LocateMyFSM("Bee").SetState("Pause");
            }
            else if (goName.Contains("Zote Balloon"))
            {
                gameObject.AddComponent<VolatileZoteling>();
            }
            else if (goName.Contains("Zote Crew Fat"))
            {
                gameObject.AddComponent<FatZote>();
            }
            else if (goName.Contains("Zote Crew Normal"))
            {
                gameObject.AddComponent<ZoteTheMighty>();
            }
            else if (goName.Contains("Zote Crew Tall"))
            {
                gameObject.AddComponent<TallZote>();
            }
            else if (goName.Contains("Zote Fluke"))
            {
                gameObject.AddComponent<ZoteFluke>();
            }
            else if (goName.Contains("Zote Salubra"))
            {
                gameObject.AddComponent<ZoteSalubra>();
            }
            else if (goName.Contains("Zote Thwomp"))
            {
                gameObject.AddComponent<ZoteThwomp>();
            }
            else if (goName.Contains("Zote Turret"))
            {
                gameObject.AddComponent<ZoteTurret>();
            }
            else if (goName.Contains("Zoteling"))
            {
                gameObject.AddComponent<Zoteling>();
            }
            else if (goName.Contains("Absolute Radiance"))
            {
                gameObject.AddComponent<AbsoluteRadiance>();
            }
            else if (goName.Contains("Infected Knight"))
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
            else if (goName.Contains("Fluke Mother"))
            {
                gameObject.AddComponent<Flukemarm>();
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
            else if (goName.Contains("Grimm Boss") && !goName.Contains("Nightmare"))
            {
                GameObject spikeHolder = Instantiate(CustomTrial.GameObjects["grimmspikeholder"], new Vector2(ArenaInfo.CenterX, ArenaInfo.BottomY - 3), Quaternion.identity);
                spikeHolder.SetActive(true);
                gameObject.AddComponent<TroupeMasterGrimm>();
            }
            else if (goName.Contains("Lancer"))
            {
                gameObject.AddComponent<Lancer>();
            }
            else if (goName.Contains("Lobster"))
            {
                gameObject.AddComponent<Lobster>();
            }
            else if (goName.Contains("Giant Buzzer Col"))
            {
                gameObject.AddComponent<VengeflyKing>();
            }
            else if (goName.Contains("Giant Fly"))
            {
                gameObject.AddComponent<GruzMother>();
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
                //Instantiate(CustomTrial.GameObjects["needle"], gameObject.transform);
                gameObject.AddComponent<HornetProtector>();
            }
            else if (goName.Contains("Hornet Boss 2"))
            {
                //Instantiate(CustomTrial.GameObjects["needle"], gameObject.transform);
                gameObject.AddComponent<HornetSentinel>();
            }
            else if (goName.Contains("Lost Kin"))
            {
                gameObject.AddComponent<LostKin>();
            }
            else if (goName.Contains("Mantis Lord"))
            {
                gameObject.AddComponent<MantisLord>();
            }
            else if (goName.Contains("Mantis Traitor Lord"))
            {
                gameObject.AddComponent<TraitorLord>();
            }
            else if (goName.Contains("Mega Moss Charger"))
            {
                gameObject.AddComponent<MassiveMossCharger>();
            }
            else if (goName.Contains("Oro") || goName.Contains("Mato"))
            {
                gameObject.AddComponent<Nailmaster>();
            }
            else if (goName.Contains("Nightmare Grimm Boss"))
            {
                GameObject spikeHolder = Instantiate(CustomTrial.GameObjects["nightmaregrimmspikeholder"], new Vector2(ArenaInfo.CenterX, ArenaInfo.BottomY - 3), Quaternion.identity);
                spikeHolder.SetActive(true);
                gameObject.AddComponent<NightmareKingGrimm>();
            }
            else if (goName.Contains("Mimic Spider"))
            {
                gameObject.AddComponent<Nosk>();
            }
            else if (goName.Contains("Hornet Nosk"))
            {
                gameObject.AddComponent<WingedNosk>();
            }
            else if (goName.Contains("Mega Fat Bee"))
            {
                gameObject.AddComponent<Oblobble>();
            }
            else if (goName.Contains("Sheo Boss"))
            {
                gameObject.AddComponent<PaintmasterSheo>();
            }
            else if (goName.Contains("Sly Boss"))
            {
                gameObject.AddComponent<GreatNailsageSly>();
            }
            else if (goName.Contains("Mage Lord") && !goName.Contains("Dream"))
            {
                gameObject.AddComponent<SoulMaster>();
            }
            else if (goName.Contains("Dream Mage Lord"))
            {
                gameObject.AddComponent<SoulTyrant>();
            }
            else if (goName.Contains("Mage Knight"))
            {
                gameObject.AddComponent<SoulWarrior>();
            }
            else if (goName.Contains("Slash Spider"))
            {
                gameObject.AddComponent<StalkingDevout>();
            }
            else if (goName.Contains("Mega Jellyfish GG"))
            {
                gameObject.AddComponent<Uumuu>();
                GameObject jellyfishSpawner = Instantiate(CustomTrial.GameObjects["jellyfishspawner"], new Vector2(ArenaInfo.CenterX, ArenaInfo.CenterY), Quaternion.identity);
                jellyfishSpawner.SetActive(true);
                jellyfishSpawner.AddComponent<JellyfishSpawner>();
                GameObject multizaps = Instantiate(CustomTrial.GameObjects["megajellyfishmultizaps"], new Vector2(ArenaInfo.CenterX, ArenaInfo.CenterY), Quaternion.identity);
                multizaps.SetActive(true);
            }
            else if (goName.Contains("Black Knight"))
            {
                gameObject.AddComponent<WatcherKnight>();
            }
            else if (goName.Contains("White Defender"))
            {
                gameObject.AddComponent<WhiteDefender>();
            }
            else if (goName.Contains("Hollow Knight Boss"))
            {
                gameObject.AddComponent<TheHollowKnight>();
            }
            else if (goName.Contains("Radiance") && !goName.Contains("Absolute"))
            {
                gameObject.AddComponent<TheRadiance>();
            }
            else if (goName.Contains("Flamebearer Large"))
            {
                gameObject.AddComponent<Grimmkin>().grimmchildLevel = 3;
            }
            else if (goName.Contains("Flamebearer Med"))
            {
                gameObject.AddComponent<Grimmkin>().grimmchildLevel = 2;
            }
            else if (goName.Contains("Flamebearer Small"))
            {
                gameObject.AddComponent<Grimmkin>().grimmchildLevel = 1;
            }
            else
            {
                _fsm.SetState(_fsm.Fsm.StartState);
            }
        }
    }
}