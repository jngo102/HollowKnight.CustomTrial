using CustomTrial.Classes;
using CustomTrial.Dialogue;
using Modding;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using ModCommon;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CustomTrial
{
    public class CustomTrial : Mod
    {
        private static readonly string _xmlDir = Application.dataPath + "/Managed/Mods/CustomTrial";
        private static readonly string _xmlPath = Path.Combine(_xmlDir, "WaveSequence.xml");

        public static Dictionary<string, GameObject> GameObjects = new Dictionary<string, GameObject>();
        public static List<GameObject> Crowds = new List<GameObject>();

        private Dictionary<string, (string, string)> _preloadDictionary = new Dictionary<string, (string, string)>
        {
            ["Lesser Mawlek"] = ("Abyss_17", "Lesser Mawlek"),
            ["Shadow Creeper"] = ("Abyss_20", "Abyss Crawler"),
            ["Infected Balloon"] = ("Abyss_20", "Parasite Balloon (6)"),
            ["Mawlurk"] = ("Abyss_20", "Mawlek Turret"),
            ["Vengefly"] = ("Cliffs_02", "Buzzer"),
            ["Crawlid"] = ("Cliffs_02", "Crawler"),
            ["Husk Bully"] = ("Cliffs_02", "Zombie Barger"),
            ["Baldur"] = ("Crossroads_ShamanTemple", "_Enemies/Roller"),
            ["Elder Baldur"] = ("Crossroads_ShamanTemple", "Battle Scene/Blocker"),
            ["Mender Bug"] = ("Crossroads_01", "_Scenery/Mender Bug"),
            ["Volatile Husk"] = ("Crossroads_01", "Infected Parent/Bursting Zombie"),
            ["Volatile Gruzzer"] = ("Crossroads_07", "Infected Parent/Bursting Bouncer"),
            ["Gruzzer"] = ("Crossroads_07", "Uninfected Parent/Fly"),
            ["Slobbering Husk"] = ("Crossroads_15", "Infected Parent/Spitting Zombie"),
            ["Husk Warrior"] = ("Crossroads_15", "_Enemies/Zombie Shield"),
            ["Aspid Mother"] = ("Crossroads_19", "_Enemies/Hatcher"),
            ["Aspid Hunter"] = ("Crossroads_19", "_Enemies/Spitter"),
            ["Leaping Husk"] = ("Crossroads_19", "_Enemies/Zombie Leaper"),
            ["Wandering Husk"] = ("Crossroads_19", "_Enemies/Zombie Runner"),
            ["Furious Vengefly"] = ("Crossroads_21", "infected_event/Angry Buzzer"),
            ["Husk Guard"] = ("Crossroads_21", "non_infected_event/Zombie Guard"),
            ["Primal Aspid"] = ("Deepnest_East_06", "Super Spitter"),
            ["Hopper"] = ("Deepnest_East_06", "Hopper"),
            ["Great Hopper"] = ("Deepnest_East_06", "Hopper Spawn/Giant Hopper"),
            ["Boofly"] = ("Deepnest_East_07", "Blow Fly"),
            ["Belfly"] = ("Deepnest_East_07", "Ceiling Dropper"),
            ["Dirtcarver"] = ("Deepnest_17", "Baby Centipede"),
            ["Carver Hatcher"] = ("Deepnest_26b", "Centipede Hatcher (4)"),
            ["Stalking Devout"] = ("Deepnest_34", "Slash Spider"),
            ["Deephunter"] = ("Deepnest_34", "Spider Mini"),
            ["Corpse Creeper"] = ("Deepnest_34", "Zombie Hornhead Sp"),
            ["Grub Mimic"] = ("Deepnest_36", "Grub Mimic Top"),
            ["Little Weaver"] = ("Deepnest_41", "Spider Flyer"),
            ["Deepling"] = ("Deepnest_41", "Tiny Spider"),
            ["Mosscreep"] = ("Fungus1_01", "Moss Walker"),
            ["Mosskin"] = ("Fungus1_01", "Mossman_Runner"),
            ["Volatile Mosskin"] = ("Fungus1_01", "Mossman_Shaker"),
            ["Squit"] = ("Fungus1_07", "Mosquito"),
            ["Duranda"] = ("Fungus1_09", "Acid Flyer"),
            ["Durandoo"] = ("Fungus1_12", "Acid Walker"),
            ["Maskfly"] = ("Fungus1_12", "Pigeon"),
            ["Gulka"] = ("Fungus1_12", "Plant Turret"),
            ["Obble"] = ("Fungus1_19", "_Enemies/Fat Fly"),
            ["Moss Charger"] = ("Fungus1_21", "Moss Charger"),
            ["Moss Knight"] = ("Fungus1_21", "Battle Scene/Moss Knight"),
            ["Mantis Warrior"] = ("Fungus2_12", "Mantis"),
            ["Mantis Youth"] = ("Fungus2_12", "Mantis Flyer Child"),
            ["Fungified Husk"] = ("Fungus2_18", "Zombie Fungus A"),
            ["Fungling"] = ("Fungus2_18", "_Scenery/Fungoon Baby"),
            ["Fungoon"] = ("Fungus2_18", "_Scenery/Fungus Flyer"),
            ["Ambloom"] = ("Fungus2_30", "Fung Crawler"),
            ["Shrumeling"] = ("Fungus2_30", "Mushroom Baby"),
            ["Shrumal Ogre"] = ("Fungus2_30", "Mushroom Brawler"),
            ["Shrumal Warrior"] = ("Fungus2_30", "Mushroom Roller"),
            ["Sporg"] = ("Fungus2_30", "Mushroom Turret"),
            ["Spiny Husk"] = ("Fungus3_22", "Garden Zombie"),
            ["Mantis Petra"] = ("Fungus3_22", "Mantis Heavy Flyer"),
            ["Mossy Vagabond"] = ("Fungus3_39", "Moss Knight Fat"),
            ["Mantis Traitor"] = ("Fungus3_39", "Shiny Spawner/Mantis Heavy Spawn"),
            ["Mossfly"] = ("Fungus3_22", "Moss Flyer (3)"),
            ["Loodle"] = ("Fungus3_48", "Grass Hopper"),
            ["Aluba"] = ("Fungus3_48", "Lazy Flyer Enemy"),
            ["Ooma"] = ("Fungus3_27", "Jellyfish"),
            ["Uoma"] = ("Fungus3_27", "Jellyfish Baby"),
            ["Fool Eater"] = ("Fungus3_48", "Plant Trap"),
            ["Flukemunga"] = ("GG_Pipeway", "Fat Fluke"),
            ["Hiveling"] = ("Hive_03", "Bee Hatchling Ambient (22)"),
            ["Hive Soldier"] = ("Hive_03", "Bee Stinger (8)"),
            ["Hive Guardian"] = ("Hive_03", "Big Bee (2)"),
            ["Husk Hive"] = ("Hive_01", "Zombie Hive"),
            ["Glimback"] = ("Mines_07", "Crystal Crawler"),
            ["Crystal Hunter"] = ("Mines_07", "Crystal Flyer"),
            ["Crystal Crawler"] = ("Mines_11", "Crystallised Lazer Bug"),
            ["Shardmite"] = ("Mines_11", "Mines Crawler"),
            ["Husk Miner"] = ("Mines_11", "Zombie Miner 1"),
            ["Crystallised Husk"] = ("Mines_23", "Zombie Beam Miner"),
            ["Entombed Husk"] = ("RestingGrounds_10", "Grave Zombie"),
            ["Soul Twister"] = ("Room_Colosseum_Gold", "Colosseum Manager/Waves/Wave 22/Mage"),
            ["Volt Twister"] = ("Room_Colosseum_Gold", "Colosseum Manager/Waves/Wave 25/Electric Mage New"),
            ["Great Husk Sentry"] = ("Ruins_House_01", "Battle Scene/Great Shield Zombie"),
            ["Gorgeous Husk"] = ("Ruins_House_02", "Gorgeous Husk"),
            ["Cowardly Husk"] = ("Ruins_House_02", "Royal Zombie Coward"),
            ["Gluttonous Husk"] = ("Ruins_House_02", "Royal Zombie Fat"),
            ["Husk Dandy"] = ("Ruins_House_02", "Royal Zombie 1 (1)"),
            ["Folly"] = ("Ruins1_32", "Mage Balloon"),
            ["Mistake"] = ("Ruins1_32", "Mage Blob 1"),
            ["Heavy Sentry"] = ("Ruins2_09", "Battle Scene/Wave 1/Ruins Sentry Fat"),
            ["Husk Sentry"] = ("Ruins2_09", "Battle Scene/Wave 1/Ruins Sentry 1"),
            ["Winged Sentry"] = ("Ruins2_09", "Battle Scene/Wave 2/Ruins Flying Sentry"),
            ["Lance Sentry"] = ("Ruins2_09", "Battle Scene/Wave 2/Ruins Flying Sentry Javelin"),
            ["Pilflip"] = ("Waterways_02", "Flip Hopper"),
            ["Flukefey"] = ("Waterways_02", "Fluke Fly"),
            ["Flukemon"] = ("Waterways_04b", "Flukeman"),
            ["Hwurmp"] = ("Waterways_04b", "Inflater"),
            ["Wingmould"] = ("White_Palace_01", "White Palace Fly"),
            ["Royal Retainer"] = ("White_Palace_03_hub", "Enemy"),
            ["Kingsmould"] = ("White_Palace_11", "Royal Gaurd"),
            ["Volatile Zoteling"] = ("GG_Mighty_Zote", "Battle Control/Zote Balloon Ordeal"),
            ["Zoteling"] = ("GG_Mighty_Zote", "Battle Control/Zotelings/Ordeal Zoteling"),
            ["Fat Zote"] = ("GG_Mighty_Zote", "Battle Control/Fat Zotes/Zote Crew Fat (1)"),
            ["Tall Zote"] = ("GG_Mighty_Zote", "Battle Control/Tall Zotes/Zote Crew Tall"),
            ["Zote Salubra"] = ("GG_Mighty_Zote", "Battle Control/Zote Salubra"),
            ["Zote the Mighty"] = ("GG_Mighty_Zote", "Battle Control/Dormant Warriors/Zote Crew Normal (1)"),
            ["Zote Turret"] = ("GG_Mighty_Zote", "Battle Control/Extra Zotes/Zote Turret"),
            ["Zote Fluke"] = ("GG_Mighty_Zote", "Battle Control/Zote Fluke"),
            ["Zote Thwomp"] = ("GG_Mighty_Zote", "Battle Control/Zote Thwomp"),

            ["Broken Vessel"] = ("GG_Broken_Vessel", "Infected Knight"),
            ["Brooding Mawlek"] = ("GG_Brooding_Mawlek", "Battle Scene/Mawlek Body"),
            ["The Collector"] = ("GG_Collector_V", "Battle Scene/Jar Collector"),
            ["Crystal Guardian"] = ("GG_Crystal_Guardian", "Mega Zombie Beam Miner (1)"),
            ["Enraged Guardian"] = ("GG_Crystal_Guardian_2", "Battle Scene/Zombie Beam Miner Rematch"),
            ["Dung Defender"] = ("GG_Dung_Defender", "Dung Defender"),
            ["Failed Champion"] = ("GG_Failed_Champion", "False Knight Dream"),
            ["False Knight"] = ("GG_False_Knight", "Battle Scene/False Knight New"),
            ["Flukemarm"] = ("GG_Flukemarm", "Fluke Mother"),
            ["Galien"] = ("GG_Ghost_Galien", "Warrior/Ghost Warrior Galien"),
            ["Gorb"] = ("Cliffs_02_boss", "Warrior/Ghost Warrior Slug"),
            ["Elder Hu"] = ("GG_Ghost_Hu", "Warrior/Ghost Warrior Hu"),
            ["Markoth"] = ("GG_Ghost_Markoth", "Warrior/Ghost Warrior Markoth"),
            ["Marmu"] = ("GG_Ghost_Marmu", "Warrior/Ghost Warrior Marmu"),
            ["No Eyes"] = ("GG_Ghost_No_Eyes", "Warrior/Ghost Warrior No Eyes"),
            ["Xero"] = ("GG_Ghost_Xero", "Warrior/Ghost Warrior Xero"),
            ["Lancer"] = ("GG_God_Tamer", "Entry Object/Lancer"),
            ["Lobster"] = ("GG_God_Tamer", "Entry Object/Lobster"),
            ["Grey Prince Zote"] = ("GG_Grey_Prince_Zote", "Grey Prince"),
            ["Troupe Master Grimm"] = ("GG_Grimm", "Grimm Scene/Grimm Boss"),
            ["Grimm Spike Holder"] = ("GG_Grimm", "Grimm Spike Holder"),
            ["Nightmare King Grimm"] = ("GG_Grimm_Nightmare", "Grimm Control/Nightmare Grimm Boss"),
            ["Nightmare Grimm Spike Holder"] = ("GG_Grimm_Nightmare", "Grimm Spike Holder"),
            ["Gruz Mother"] = ("GG_Gruz_Mother", "_Enemies/Giant Fly"),
            ["Hive Knight"] = ("GG_Hive_Knight", "Battle Scene/Hive Knight"),
            ["Pure Vessel"] = ("GG_Hollow_Knight", "Battle Scene/HK Prime"),
            ["Hornet Protector"] = ("GG_Hornet_1", "Boss Holder/Hornet Boss 1"),
            ["Needle"] = ("GG_Hornet_1", "Boss Holder/Hornet Boss 1/Needle"),
            ["Hornet Sentinel"] = ("GG_Hornet_2", "Boss Holder/Hornet Boss 2"),
            ["Lost Kin"] = ("GG_Lost_Kin", "Lost Kin"),
            ["Pale Lurker"] = ("GG_Lurker", "Lurker Control/Pale Lurker"),
            ["Soul Warrior"] = ("GG_Mage_Knight", "Mage Knight"),
            ["Mantis Lord"] = ("GG_Mantis_Lords", "Mantis Battle/Battle Main/Mantis Lord"),
            ["Massive Moss Charger"] = ("GG_Mega_Moss_Charger", "Mega Moss Charger"),
            ["Mato"] = ("GG_Nailmasters", "Brothers/Mato"),
            ["Oro"] = ("GG_Nailmasters", "Brothers/Oro"),
            ["Nosk"] = ("GG_Nosk", "Mimic Spider"),
            ["Winged Nosk"] = ("GG_Nosk_Hornet", "Battle Scene/Hornet Nosk"),
            ["Oblobble"] = ("GG_Oblobbles", "Mega Fat Bee"),
            ["Paintmaster Sheo"] = ("GG_Painter", "Battle Scene/Sheo Boss"),
            ["Absolute Radiance"] = ("GG_Radiance", "Boss Control/Absolute Radiance"),
            ["Great Nailsage Sly"] = ("GG_Sly", "Battle Scene/Sly Boss"),
            ["Soul Master"] = ("GG_Soul_Master", "Mage Lord"),
            ["Soul Tyrant"] = ("GG_Soul_Tyrant", "Dream Mage Lord"),
            ["Traitor Lord"] = ("GG_Traitor_Lord", "Battle Scene/Wave 3/Mantis Traitor Lord"),
            ["Uumuu"] = ("GG_Uumuu", "Mega Jellyfish GG"),
            ["Jellyfish Spawner"] = ("GG_Uumuu", "Jellyfish Spawner"),
            ["Mega Jellyfish Multizaps"] = ("GG_Uumuu", "Mega Jellyfish Multizaps"),
            ["Hatcher Cage"] = ("GG_Flukemarm", "Hatcher Cage (2)"),
            ["Vengefly King"] = ("GG_Vengefly", "Giant Buzzer Col"),
            ["Watcher Knight"] = ("GG_Watcher_Knights", "Battle Control/Black Knight 1"),
            ["White Defender"] = ("GG_White_Defender", "White Defender"),
            ["The Hollow Knight"] = ("Room_Final_Boss_Core", "Boss Control/Hollow Knight Boss"),
            ["The Radiance"] = ("Dream_Final_Boss", "Boss Control/Radiance"),
            ["Sibling"] = ("Abyss_15", "Shade Sibling (14)"),
            ["Grimmkin Novice"] = ("Mines_10", "Flamebearer Spawn"),
            ["Grimmkin Master"] = ("RestingGrounds_06", "Flamebearer Spawn"),
            ["Grimmkin Nightmare"] = ("Hive_03", "Flamebearer Spawn"),
        };
        
        public static BattleControl BattleControl = new BattleControl();
        public static CustomTrial Instance { get; private set; }

        public override string GetVersion()
        {
            return "0.0.1";
        }
        
        private List<(string, string)> GetEnemyPreloads()
        {
            if (!Directory.Exists(_xmlDir))
            {
                Log("Creating CustomTrial/ directory");
                Directory.CreateDirectory(_xmlDir);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(BattleControl));
            if (!File.Exists(_xmlPath))
            {
                Log("Creating WaveSequence.xml");
                Wave exampleWave1 = new Wave(
                    new List<Enemy>
                    {
                        new Enemy("Primal Aspid", 50, new Vector2(90, 10)),
                        new Enemy("Great Hopper", 100, new Vector2(110, 10)),
                    },
                    new List<Vector2> { new Vector2(100, 10) },
                    "Cheer",
                    "1",
                    1.0f,
                    0.0f,
                    0.0f,
                    0.0f,
                    0.0f,
                    false);
                Wave exampleWave2 = new Wave(
                    new List<Enemy>
                    {
                        new Enemy("Flukemunga", 200, new Vector2(94, 8)), 
                        new Enemy("Obble", 350, new Vector2(95, 10)),
                        new Enemy("Ooma", 1, new Vector2(110, 12)),
                    },
                    new List<Vector2> { new Vector2(100, 10) },
                    "Laugh",
                    "5",
                    1.0f,
                    1.0f,
                    10.0f,
                    10.0f,
                    10.0f,
                    true);
                BattleControl = new BattleControl();
                BattleControl.AddWave(exampleWave1);
                BattleControl.AddWave(exampleWave2);
                StreamWriter writer = new StreamWriter(_xmlPath);
                serializer.Serialize(writer.BaseStream, BattleControl);
                writer.Close();
            }

            FileStream stream = new FileStream(_xmlPath, FileMode.OpenOrCreate);
            StreamReader reader = new StreamReader(stream);
            BattleControl = (BattleControl)serializer.Deserialize(reader);
            BattleControl.AddWave(new Wave());

            List<(string, string)> preloads = new List<(string, string)>();
            foreach (Wave wave in BattleControl.Waves)
            {
                foreach (Enemy enemy in wave.Enemies)
                {
                    if (!preloads.Contains(_preloadDictionary[enemy.Name]))
                    {
                        preloads.Add(_preloadDictionary[enemy.Name]);
                        GameObjects.Add(enemy.Name, null);
                    }
                    if (enemy.Name.Contains("Flukemarm") && !GameObjects.ContainsKey("Hatcher Cage"))
                    {
                        preloads.Add(_preloadDictionary["Hatcher Cage"]);
                        GameObjects.Add("Hatcher Cage", null);
                    }
                    else if (enemy.Name.Contains("Hornet") && !GameObjects.ContainsKey("Needle"))
                    {
                        //preloads.Add(_preloadDictionary["Needle"]);
                        //GameObjects.Add("Needle", null);
                    }
                    else if (enemy.Name.Contains("Uumuu") && !GameObjects.ContainsKey("Jellyfish Spawner") && !GameObjects.ContainsKey("Mega Jellyfish Multizaps"))
                    {
                        preloads.Add(_preloadDictionary["Jellyfish Spawner"]);
                        GameObjects.Add("Jellyfish Spawner", null);
                        preloads.Add(_preloadDictionary["Mega Jellyfish Multizaps"]);
                        GameObjects.Add("Mega Jellyfish Multizaps", null);
                    }
                    else if (enemy.Name.Contains("Troupe Master Grimm") && !GameObjects.ContainsKey("Grimm Spike Holder"))
                    {
                        preloads.Add(_preloadDictionary["Grimm Spike Holder"]);
                        GameObjects.Add("Grimm Spike Holder", null);
                    }
                    else if (enemy.Name.Contains("Nightmare King Grimm") && !GameObjects.ContainsKey("Nightmare Grimm Spike Holder"))
                    {
                        preloads.Add(_preloadDictionary["Nightmare Grimm Spike Holder"]);
                        GameObjects.Add("Nightmare Grimm Spike Holder", null);
                    }
                }
            }

            return preloads;
        }
        
        public override List<(string, string)> GetPreloadNames()
        {
            List<(string, string)> preloads = GetEnemyPreloads();

            return preloads;
        }

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            // Enemies
            Dictionary<string, GameObject> gameObjects = new Dictionary<string, GameObject>();
            foreach (KeyValuePair<string, GameObject> pair in GameObjects)
            {
                string goName = pair.Key;
                if (_preloadDictionary.Keys.Contains(goName))
                {
                    (string sceneName, string enemyPath) = _preloadDictionary[goName];
                    GameObject gameObject = preloadedObjects[sceneName][enemyPath];
                    if (goName.Contains("Grimmkin"))
                    {
                        gameObject = gameObject.LocateMyFSM("Spawn Control").Fsm.GetFsmGameObject("Grimmkin Obj").Value;
                    }
                    gameObjects.Add(goName, gameObject);
                }
            }

            foreach (KeyValuePair<string, GameObject> pair in gameObjects)
            {
                GameObjects[pair.Key] = pair.Value;
            }

            Instance = this;

            Log("Finished Initialization");
            
            ModHooks.Instance.LanguageGetHook += OnLangGet;
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private string OnLangGet(string key, string sheetTitle)
        {
            switch (key)
            {
                case "CUSTOM_TRIAL_DIALOGUE":
                    return "Test<page>" + "Test 2<page>";
                case "START_CUSTOM_TRIAL":
                    return "Start custom trial?";
                default:
                    return Language.Language.GetInternal(key, sheetTitle);
            }
        }
        
        private void OnSceneChanged(Scene prevScene, Scene nextScene)
        {
            if (nextScene.name == "Room_Colosseum_01")
            {
                GameObject.Find("Little Fool NPC").AddComponent<LittleFool>();
                GameObject.Find("Text YN").AddComponent<TextYN>();
            }
            else if (nextScene.name == "Room_Colosseum_Gold")
            {
                Crowds.Clear();
                foreach (GameObject crowd in GameObject.FindObjectsOfType<GameObject>()
                    .Where(go => go.name.Contains("Colosseum Crowd NPC")))
                {
                    Crowds.Add(crowd);
                }
                GameObject.Find("Colosseum Manager").AddComponent<ColosseumManager>();
            }
        }
    }
}