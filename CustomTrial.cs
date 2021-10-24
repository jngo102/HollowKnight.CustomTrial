using CustomTrial.Classes;
using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UObject = UnityEngine.Object;

namespace CustomTrial
{
    public class CustomTrial : Mod, IGlobalSettings<GlobalSettings>
    {
        internal static CustomTrial Instance;

        public static Dictionary<string, GameObject> GameObjects = new();
        public static List<GameObject> Crowds = new();

        private static GlobalSettings _globalSettings = new();
        public static GlobalSettings GlobalSettings => _globalSettings;

        public override string GetVersion() => "0.0.0.1";

        private Dictionary<string, (string, string)> _preloadDictionary = new()
        {
            ["shadowcreeper"] = ("Abyss_20", "Abyss Crawler"),
            ["infectedballoon"] = ("Abyss_20", "Parasite Balloon (6)"),
            ["maggot"] = ("Crossroads_10_boss_defeated", "Prayer Room/Prayer Slug"),
            ["mawlurk"] = ("Abyss_20", "Mawlek Turret"),
            ["vengefly"] = ("Cliffs_02", "Buzzer"),
            ["crawlid"] = ("Cliffs_02", "Crawler"),
            ["huskbully"] = ("Cliffs_02", "Zombie Barger"),
            ["baldur"] = ("Crossroads_ShamanTemple", "_Enemies/Roller"),
            ["elderbaldur"] = ("Crossroads_ShamanTemple", "Battle Scene/Blocker"),
            ["menderbug"] = ("Crossroads_01", "_Scenery/Mender Bug"),
            ["violenthusk"] = ("Crossroads_01", "Infected Parent/Bursting Zombie"),
            ["volatilegruzzer"] = ("Crossroads_07", "Infected Parent/Bursting Bouncer"),
            ["gruzzer"] = ("Crossroads_07", "Uninfected Parent/Fly"),
            ["slobberinghusk"] = ("Crossroads_15", "Infected Parent/Spitting Zombie"),
            ["huskwarrior"] = ("Crossroads_15", "_Enemies/Zombie Shield"),
            ["aspidmother"] = ("Crossroads_19", "_Enemies/Hatcher"),
            ["aspidhunter"] = ("Crossroads_19", "_Enemies/Spitter"),
            ["leapinghusk"] = ("Crossroads_19", "_Enemies/Zombie Leaper"),
            ["wanderinghusk"] = ("Crossroads_19", "_Enemies/Zombie Runner"),
            ["huskguard"] = ("Crossroads_21", "non_infected_event/Zombie Guard"),
            ["hopper"] = ("Deepnest_East_06", "Hopper"),
            ["greathopper"] = ("Deepnest_East_06", "Hopper Spawn/Giant Hopper"),
            ["boofly"] = ("Deepnest_East_07", "Blow Fly"),
            ["dirtcarver"] = ("Deepnest_17", "Baby Centipede"),
            ["carverhatcher"] = ("Deepnest_26b", "Centipede Hatcher (4)"),
            ["stalkingdevout"] = ("Deepnest_34", "Slash Spider"),
            ["deephunter"] = ("Deepnest_34", "Spider Mini"),
            ["corpsecreeper"] = ("Deepnest_34", "Zombie Hornhead Sp"),
            ["grubmimic"] = ("Deepnest_36", "Grub Mimic Top"),
            ["littleweaver"] = ("Deepnest_41", "Spider Flyer"),
            ["deepling"] = ("Deepnest_41", "Tiny Spider"),
            ["mosscreep"] = ("Fungus1_01", "Moss Walker"),
            ["mosskin"] = ("Fungus1_01", "Mossman_Runner"),
            ["volatilemosskin"] = ("Fungus1_01", "Mossman_Shaker"),
            ["squit"] = ("Fungus1_07", "Mosquito"),
            ["duranda"] = ("Fungus1_09", "Acid Flyer"),
            ["durandoo"] = ("Fungus1_12", "Acid Walker"),
            ["maskfly"] = ("Fungus1_12", "Pigeon"),
            ["gulka"] = ("Fungus1_12", "Plant Turret"),
            ["obble"] = ("Fungus1_19", "_Enemies/Fat Fly"),
            ["mosscharger"] = ("Fungus1_21", "Moss Charger"),
            ["mossknight"] = ("Fungus1_21", "Battle Scene/Moss Knight"),
            ["mantiswarrior"] = ("Fungus2_12", "Mantis"),
            ["mantisyouth"] = ("Fungus2_12", "Mantis Flyer Child"),
            ["fungifiedhusk"] = ("Fungus2_18", "Zombie Fungus A"),
            ["fungling"] = ("Fungus2_18", "_Scenery/Fungoon Baby"),
            ["fungoon"] = ("Fungus2_18", "_Scenery/Fungus Flyer"),
            ["ambloom"] = ("Fungus2_30", "Fung Crawler"),
            ["shrumeling"] = ("Fungus2_30", "Mushroom Baby"),
            ["shrumalogre"] = ("Fungus2_30", "Mushroom Brawler"),
            ["shrumalwarrior"] = ("Fungus2_30", "Mushroom Roller"),
            ["sporg"] = ("Fungus2_30", "Mushroom Turret"),
            ["spinyhusk"] = ("Fungus3_22", "Garden Zombie"),
            ["mossyvagabond"] = ("Fungus3_39", "Moss Knight Fat"),
            ["mossfly"] = ("Fungus3_22", "Moss Flyer (3)"),
            ["loodle"] = ("Fungus3_48", "Grass Hopper"),
            ["aluba"] = ("Fungus3_48", "Lazy Flyer Enemy"),
            ["ooma"] = ("Fungus3_27", "Jellyfish"),
            ["uoma"] = ("Fungus3_27", "Jellyfish Baby"),
            ["fooleater"] = ("Fungus3_48", "Plant Trap"),
            ["flukemunga"] = ("GG_Pipeway", "Fat Fluke"),
            ["hiveling"] = ("Hive_03", "Bee Hatchling Ambient (22)"),
            ["hivesoldier"] = ("Hive_03", "Bee Stinger (8)"),
            ["hiveguardian"] = ("Hive_03", "Big Bee (2)"),
            ["huskhive"] = ("Hive_01", "Zombie Hive"),
            ["glimback"] = ("Mines_07", "Crystal Crawler"),
            ["crystalhunter"] = ("Mines_07", "Crystal Flyer"),
            ["crystalcrawler"] = ("Mines_11", "Crystallised Lazer Bug"),
            ["shardmite"] = ("Mines_11", "Mines Crawler"),
            ["huskminer"] = ("Mines_11", "Zombie Miner 1"),
            ["crystallisedhusk"] = ("Mines_23", "Zombie Beam Miner"),
            ["entombedhusk"] = ("RestingGrounds_10", "Grave Zombie"),
            ["greathusksentry"] = ("Ruins_House_01", "Battle Scene/Great Shield Zombie"),
            ["gorgeoushusk"] = ("Ruins_House_02", "Gorgeous Husk"),
            ["howardlyhusk"] = ("Ruins_House_02", "Royal Zombie Coward"),
            ["gluttonoushusk"] = ("Ruins_House_02", "Royal Zombie Fat"),
            ["huskdandy"] = ("Ruins_House_02", "Royal Zombie 1 (1)"),
            ["folly"] = ("Ruins1_32", "Mage Balloon"),
            ["mistake"] = ("Ruins1_32", "Mage Blob 1"),
            ["heavysentry"] = ("Ruins2_09", "Battle Scene/Wave 1/Ruins Sentry Fat"),
            ["husksentry"] = ("Ruins2_09", "Battle Scene/Wave 1/Ruins Sentry 1"),
            ["wingedsentry"] = ("Ruins2_09", "Battle Scene/Wave 2/Ruins Flying Sentry"),
            ["lancesentry"] = ("Ruins2_09", "Battle Scene/Wave 2/Ruins Flying Sentry Javelin"),
            ["pilflip"] = ("Waterways_02", "Flip Hopper"),
            ["flukefey"] = ("Waterways_02", "Fluke Fly"),
            ["flukemon"] = ("Waterways_04b", "Flukeman"),
            ["hwurmp"] = ("Waterways_04b", "Inflater"),
            ["wingmould"] = ("White_Palace_01", "White Palace Fly"),
            ["royalretainer"] = ("White_Palace_03_hub", "Enemy"),
            ["kingsmould"] = ("White_Palace_11", "Royal Gaurd"),
            ["volatilezoteling"] = ("GG_Mighty_Zote", "Battle Control/Zote Balloon Ordeal"),
            ["zoteling"] = ("GG_Mighty_Zote", "Battle Control/Zotelings/Ordeal Zoteling"),
            ["fatzote"] = ("GG_Mighty_Zote", "Battle Control/Fat Zotes/Zote Crew Fat (1)"),
            ["tallzote"] = ("GG_Mighty_Zote", "Battle Control/Tall Zotes/Zote Crew Tall"),
            ["zotesalubra"] = ("GG_Mighty_Zote", "Battle Control/Zote Salubra"),
            ["zotethemighty"] = ("GG_Mighty_Zote", "Battle Control/Dormant Warriors/Zote Crew Normal (1)"),
            ["zoteturret"] = ("GG_Mighty_Zote", "Battle Control/Extra Zotes/Zote Turret"),
            ["zotefluke"] = ("GG_Mighty_Zote", "Battle Control/Zote Fluke"),
            ["zotethwomp"] = ("GG_Mighty_Zote", "Battle Control/Zote Thwomp"),

            ["brokenvessel"] = ("GG_Broken_Vessel", "Infected Knight"),
            ["broodingmawlek"] = ("GG_Brooding_Mawlek", "Battle Scene/Mawlek Body"),
            ["thecollector"] = ("GG_Collector_V", "Battle Scene/Jar Collector"),
            ["crystalguardian"] = ("GG_Crystal_Guardian", "Mega Zombie Beam Miner (1)"),
            ["enragedguardian"] = ("GG_Crystal_Guardian_2", "Battle Scene/Zombie Beam Miner Rematch"),
            ["dungdefender"] = ("GG_Dung_Defender", "Dung Defender"),
            ["failedchampion"] = ("GG_Failed_Champion", "False Knight Dream"),
            ["falseknight"] = ("GG_False_Knight", "Battle Scene/False Knight New"),
            ["flukemarm"] = ("GG_Flukemarm", "Fluke Mother"),
            ["galien"] = ("GG_Ghost_Galien", "Warrior/Ghost Warrior Galien"),
            ["gorb"] = ("Cliffs_02_boss", "Warrior/Ghost Warrior Slug"),
            ["elderhu"] = ("GG_Ghost_Hu", "Warrior/Ghost Warrior Hu"),
            ["markoth"] = ("GG_Ghost_Markoth", "Warrior/Ghost Warrior Markoth"),
            ["marmu"] = ("GG_Ghost_Marmu", "Warrior/Ghost Warrior Marmu"),
            ["noeyes"] = ("GG_Ghost_No_Eyes", "Warrior/Ghost Warrior No Eyes"),
            ["xero"] = ("GG_Ghost_Xero", "Warrior/Ghost Warrior Xero"),
            ["greyprincezote"] = ("GG_Grey_Prince_Zote", "Grey Prince"),
            ["troupemastergrimm"] = ("GG_Grimm", "Grimm Scene/Grimm Boss"),
            ["grimmspikeholder"] = ("GG_Grimm", "Grimm Spike Holder"),
            ["nightmarekinggrimm"] = ("GG_Grimm_Nightmare", "Grimm Control/Nightmare Grimm Boss"),
            ["nightmaregrimmspikeholder"] = ("GG_Grimm_Nightmare", "Grimm Spike Holder"),
            ["gruzmother"] = ("GG_Gruz_Mother", "_Enemies/Giant Fly"),
            ["hiveknight"] = ("GG_Hive_Knight", "Battle Scene/Hive Knight"),
            ["purevessel"] = ("GG_Hollow_Knight", "Battle Scene/HK Prime"),
            ["hornetprotector"] = ("GG_Hornet_1", "Boss Holder/Hornet Boss 1"),
            ["needle"] = ("GG_Hornet_1", "Boss Holder/Needle"),
            ["hornetsentinel"] = ("GG_Hornet_2", "Boss Holder/Hornet Boss 2"),
            ["lostkin"] = ("GG_Lost_Kin", "Lost Kin"),
            ["palelurker"] = ("GG_Lurker", "Lurker Control/Pale Lurker"),
            ["soulwarrior"] = ("GG_Mage_Knight", "Mage Knight"),
            ["mantislord"] = ("GG_Mantis_Lords", "Mantis Battle/Battle Main/Mantis Lord"),
            ["massivemosscharger"] = ("GG_Mega_Moss_Charger", "Mega Moss Charger"),
            ["mato"] = ("GG_Nailmasters", "Brothers/Mato"),
            ["oro"] = ("GG_Nailmasters", "Brothers/Oro"),
            ["nosk"] = ("GG_Nosk", "Mimic Spider"),
            ["wingednosk"] = ("GG_Nosk_Hornet", "Battle Scene/Hornet Nosk"),
            ["oblobble"] = ("GG_Oblobbles", "Mega Fat Bee"),
            ["paintmastersheo"] = ("GG_Painter", "Battle Scene/Sheo Boss"),
            ["absoluteradiance"] = ("GG_Radiance", "Boss Control/Absolute Radiance"),
            ["greatnailsagesly"] = ("GG_Sly", "Battle Scene/Sly Boss"),
            ["soulmaster"] = ("GG_Soul_Master", "Mage Lord"),
            ["soultyrant"] = ("GG_Soul_Tyrant", "Dream Mage Lord"),
            ["traitorlord"] = ("GG_Traitor_Lord", "Battle Scene/Wave 3/Mantis Traitor Lord"),
            ["uumuu"] = ("GG_Uumuu", "Mega Jellyfish GG"),
            ["jellyfishspawner"] = ("GG_Uumuu", "Jellyfish Spawner"),
            ["megajellyfishmultizaps"] = ("GG_Uumuu", "Mega Jellyfish Multizaps"),
            ["hatchercage"] = ("GG_Flukemarm", "Hatcher Cage (2)"),
            ["vengeflyking"] = ("GG_Vengefly", "Giant Buzzer Col"),
            ["watcherknight"] = ("GG_Watcher_Knights", "Battle Control/Black Knight 1"),
            ["whitedefender"] = ("GG_White_Defender", "White Defender"),
            ["thehollowknight"] = ("Room_Final_Boss_Core", "Boss Control/Hollow Knight Boss"),
            ["thetadiance"] = ("Dream_Final_Boss", "Boss Control/Radiance"),
            ["sibling"] = ("Abyss_15", "Shade Sibling (14)"),
            ["grimmkinnovice"] = ("Mines_10", "Flamebearer Spawn"),
            ["grimmkinmaster"] = ("RestingGrounds_06", "Flamebearer Spawn"),
            ["grimmkinnightmare"] = ("Hive_03", "Flamebearer Spawn"),
        };

        private List<string> _colosseumEnemies = new()
        {
            "armoredsquit",
            "battleobble",
            "belfly",
            "deathloodle",
            "furiousvengefly",
            "heavyfool",
            "lancer",
            "lessermawlek",
            "lobster",
            "mantispetra",
            "mantistraitor",
            "primalaspid",
            "sharpbaldur",
            "sturdyfool",
            "shieldedfool",
            "soultwister",
            "volttwister",
            "wingedfool",
        };

        private List<(string, string)> GetEnemyPreloads()
        {
            if (!File.Exists(Path.Combine(Application.persistentDataPath, "CustomTrial.GlobalSettings.json")))
            {
                Log("Creating example JSON.");
                Wave exampleWave1 = new(
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
                    false
                );
                Wave exampleWave2 = new(
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
                    true
                );

                _globalSettings.SetGeoReward(100);
                _globalSettings.AddWave(exampleWave1);
                _globalSettings.AddWave(exampleWave2);
            }

            List<(string, string)> preloads = new();
            foreach (Wave wave in _globalSettings.Waves)
            {
                foreach (Enemy enemy in wave.Enemies)
                {
                    string enemyName = enemy.Name.ToLower().Replace(" ", "");
                    if (!_colosseumEnemies.Contains(enemyName) && !_preloadDictionary.ContainsKey(enemyName))
                    {
                        Log($"Enemy {enemyName} does not exist in the preloads dictionary.");
                        continue;
                    }
                    if (!_colosseumEnemies.Contains(enemyName) && !preloads.Contains(_preloadDictionary[enemyName]))
                    {
                        preloads.Add(_preloadDictionary[enemyName]);
                        GameObjects.Add(enemyName, null);
                    }
                    if (enemyName.Contains("flukemarm") && !GameObjects.ContainsKey("hatchercage"))
                    {
                        preloads.Add(_preloadDictionary["hatchercage"]);
                        GameObjects.Add("hatchercage", null);
                    }
                    else if (enemyName.Contains("hornet") && !GameObjects.ContainsKey("needle"))
                    {
                        //preloads.Add(_preloadDictionary["needle"]);
                        //GameObjects.Add("needle", null);
                    }
                    else if (enemyName.Contains("uumuu") && !GameObjects.ContainsKey("jellyfishspawner") && !GameObjects.ContainsKey("megajellyfishmultizaps"))
                    {
                        preloads.Add(_preloadDictionary["jellyfishspawner"]);
                        GameObjects.Add("jellyfishspawner", null);
                        preloads.Add(_preloadDictionary["megajellyfishmultizaps"]);
                        GameObjects.Add("megajellyfishmultizaps", null);
                    }
                    else if (enemyName.Contains("troupemastergrimm") && !GameObjects.ContainsKey("grimmspikeholder"))
                    {
                        preloads.Add(_preloadDictionary["grimmspikeholder"]);
                        GameObjects.Add("grimmspikeholder", null);
                    }
                    else if (enemyName.Contains("nightmarekinggrimm") && !GameObjects.ContainsKey("nightmaregrimmspikeholder"))
                    {
                        preloads.Add(_preloadDictionary["nightmaregrimmspikeholder"]);
                        GameObjects.Add("nightmaregrimmspikeholder", null);
                    }
                }
            }

            return preloads;
        }

        public override List<(string, string)> GetPreloadNames()
        {
            var preloads = GetEnemyPreloads();
            return preloads;
        }

        public CustomTrial() : base("Custom Trial") {}

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            Dictionary<string, GameObject> gameObjects = new();
            foreach (KeyValuePair<string, GameObject> pair in GameObjects)
            {
                string goName = pair.Key;
                if (_preloadDictionary.Keys.Contains(goName))
                {
                    (string sceneName, string enemyPath) = _preloadDictionary[goName];
                    GameObject gameObject = preloadedObjects[sceneName][enemyPath];
                    if (goName.Contains("grimmkin"))
                    {
                        gameObject = gameObject.LocateMyFSM("Spawn Control").Fsm.GetFsmGameObject("Grimmkin Obj").Value;
                    }
                    gameObjects.Add(goName, gameObject);
                }
            }

            foreach (KeyValuePair<string, GameObject> pair in gameObjects)
                GameObjects[pair.Key] = pair.Value;

            Instance = this;

            ModHooks.LanguageGetHook += OnLangGet;
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private string OnLangGet(string key, string sheetTitle, string orig)
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
                //GameObject.Find("Little Fool NPC").AddComponent<LittleFool>();
                //GameObject.Find("Text YN").AddComponent<TextYN>();
            }
            else if (nextScene.name == "Room_Colosseum_Gold")
            {
                Crowds.Clear();
                foreach (GameObject crowd in UObject.FindObjectsOfType<GameObject>()
                    .Where(go => go.name.Contains("Colosseum Crowd NPC")))
                    Crowds.Add(crowd);
                GameObject.Find("Colosseum Manager").AddComponent<ColosseumManager>();
            }
        }

        public void OnLoadGlobal(GlobalSettings globalSettings) => _globalSettings = globalSettings;

        public GlobalSettings OnSaveGlobal() => _globalSettings;
    }
}