using CustomTrial.Dialogue;
using Modding;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CustomTrial
{
    public class CustomTrial : Mod
    {
        public static Dictionary<string, GameObject> GameObjects = new Dictionary<string, GameObject>();

        public static CustomTrial Instance { get; private set; }
        
        public override string GetVersion()
        {
            return "0.0.1";
        }
        
        public override List<(string, string)> GetPreloadNames()
        {
            return new List<(string, string)>
            {
                ("Room_Colosseum_Bronze", "Colosseum Manager/Waves/Wave 1/Colosseum Cage Large"),
                ("Room_Colosseum_Bronze", "Colosseum Manager/Waves/Wave 3/Colosseum Cage Small"),
                ("Room_Colosseum_Bronze", "Colosseum Manager/Waves/Arena 1/Colosseum Platform"),
                
                ("Deepnest_East_06", "Super Spitter"),
                ("Deepnest_East_06", "Hopper"),
                ("Deepnest_East_06", "Hopper Spawn/Giant Hopper"),
                ("Fungus2_18", "_Scenery/Fungoon Baby"),
                ("Fungus2_18", "_Scenery/Fungus Flyer"),
                ("Fungus2_18", "Zombie Fungus A"),
                ("Fungus2_30", "Fung Crawler"),
                ("Fungus2_30", "Mushroom Baby"),
                ("Fungus2_30", "Mushroom Brawler"),
                ("Fungus2_30", "Mushroom Roller"),
                ("Fungus2_30", "Mushroom Turret"),
                ("Fungus3_22", "Garden Zombie"),
                ("Fungus3_22", "Mantis Heavy Flyer"),
                ("Fungus3_22", "Moss Flyer (3)"),
                
                /*("GG_Broken_Vessel", "Infected Knight"),
                ("GG_Brooding_Mawlek", "Battle Scene/Mawlek Body"),
                ("GG_Collector", "Battle Scene/Jar Collector"),
                ("GG_Crystal_Guardian", "Mega Zombie Beam Miner (1)"),
                ("GG_Crystal_Guardian_2", "Battle Scene/Zombie Beam Miner Rematch"),
                ("GG_Dung_Defender", "Dung Defender"),
                ("GG_Failed_Champion", "False Knight Dream"),
                //("GG_False_Knight", "Battle Scene/False Knight New"),
                ("GG_Flukemarm", "Fluke Mother"),
                ("GG_Ghost_Galien", "Warrior/Ghost Warrior Galien"),
                ("GG_Ghost_Gorb", "Warrior/Ghost Warrior Slug"),
                ("GG_Ghost_Hu", "Warrior/Ghost Warrior Hu"),
                ("GG_Ghost_Markoth", "Warrior/Ghost Warrior Markoth"),
                ("GG_Ghost_Marmu", "Warrior/Ghost Warrior Marmu"),
                ("GG_Ghost_No_Eyes", "Warrior/Ghost Warrior No Eyes"),
                ("GG_Ghost_Xero", "Warrior/Ghost Warrior Xero"),
                ("GG_God_Tamer", "Entry Object/Lancer"),
                ("GG_God_Tamer", "Entry Object/Lobster"),
                ("GG_Grey_Prince_Zote", "Grey Prince"),
                ("GG_Grimm", "Grimm Scene/Grimm Boss"),
                ("GG_Grimm_Nightmare", "Grimm Control/Nightmare Grimm Boss"),
                ("GG_Gruz_Mother", "_Enemies/Giant Fly"),
                ("GG_Hive_Knight", "Battle Scene/Hive Knight"),
                ("GG_Hollow_Knight", "Battle Scene/HK Prime"),
                ("GG_Hornet_1", "Boss Holder/Hornet Boss 1"),
                ("GG_Hornet_2", "Boss Holder/Hornet Boss 2"),
                ("GG_Lost_Kin", "Lost Kin"),
                ("GG_Lurker", "Lurker Control/Pale Lurker"),
                ("GG_Mage_Knight", "Mage Knight"),
                ("GG_Mantis_Lords", "Mantis Battle/Battle Main/Mantis Lord"),
                ("GG_Mega_Moss_Charger", "Mega Moss Charger"),
                ("GG_Nailmasters", "Brothers/Mato"),
                ("GG_Nailmasters", "Brothers/Oro"),
                ("GG_Nosk", "Mimic Spider"),
                ("GG_Nosk_Hornet", "Battle Scene/Hornet Nosk"),
                ("GG_Oblobbles", "Mega Fat Bee"),
                ("GG_Painter", "Battle Scene/Sheo Boss"),
                ("GG_Radiance", "Boss Control/Absolute Radiance"),
                ("GG_Sly", "Battle Scene/Sly Boss"),
                ("GG_Soul_Master", "Mage Lord"),
                ("GG_Soul_Tyrant", "Dream Mage Lord"),
                ("GG_Traitor_Lord", "Battle Scene/Wave 3/Mantis Traitor Lord"),
                ("GG_Uumuu", "Mega Jellyfish GG"),
                ("GG_Vengefly", "Giant Buzzer Col"),
                ("GG_Watcher_Knights", "Battle Control/Black Knight 1"),
                ("GG_White_Defender", "White Defender"),*/
            };
        }

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            // Environment
            GameObjects.Add("Large Cage", preloadedObjects["Room_Colosseum_Bronze"]["Colosseum Manager/Waves/Wave 1/Colosseum Cage Large"]);
            GameObjects.Add("Small Cage", preloadedObjects["Room_Colosseum_Bronze"]["Colosseum Manager/Waves/Wave 3/Colosseum Cage Small"]);
            GameObjects.Add("Platform", preloadedObjects["Room_Colosseum_Bronze"]["Colosseum Manager/Waves/Arena 1/Colosseum Platform"]);
            
            // Enemies
            GameObjects.Add("Primal Aspid", preloadedObjects["Deepnest_East_06"]["Super Spitter"]);
            GameObjects.Add("Hopper", preloadedObjects["Deepnest_East_06"]["Hopper"]);
            GameObjects.Add("Great Hopper", preloadedObjects["Deepnest_East_06"]["Hopper Spawn/Giant Hopper"]);
            GameObjects.Add("Fungoon", preloadedObjects["Fungus2_18"]["_Scenery/Fungus Flyer"]);
            GameObjects.Add("Fungling", preloadedObjects["Fungus2_18"]["_Scenery/Fungoon Baby"]);
            GameObjects.Add("Fungified Husk", preloadedObjects["Fungus2_18"]["Zombie Fungus A"]);
            GameObjects.Add("Ambloom", preloadedObjects["Fungus2_30"]["Fung Crawler"]);
            GameObjects.Add("Shrumeling", preloadedObjects["Fungus2_30"]["Mushroom Baby"]);
            GameObjects.Add("Shrumal Ogre", preloadedObjects["Fungus2_30"]["Mushroom Brawler"]);
            GameObjects.Add("Shrumal Warrior", preloadedObjects["Fungus2_30"]["Mushroom Roller"]);
            GameObjects.Add("Sporg", preloadedObjects["Fungus2_30"]["Mushroom Turret"]);
            GameObjects.Add("Spiny Husk", preloadedObjects["Fungus3_22"]["Garden Zombie"]);
            GameObjects.Add("Mantis Petra", preloadedObjects["Fungus3_22"]["Mantis Heavy Flyer"]);
            GameObjects.Add("Mossfly", preloadedObjects["Fungus3_22"]["Moss Flyer (3)"]);
            
            // Bosses
            /*GameObjects.Add("Broken Vessel", preloadedObjects["GG_Broken_Vessel"]["Infected Knight"]);
            GameObjects.Add("Brooding Mawlek", preloadedObjects["GG_Brooding_Mawlek"]["Battle Scene/Mawlek Body"]);
            GameObjects.Add("The Collector", preloadedObjects["GG_Collector"]["Battle Scene/Jar Collector"]);
            GameObjects.Add("Crystal Guardian", preloadedObjects["GG_Crystal_Guardian"]["Mega Zombie Beam Miner (1)"]);
            GameObjects.Add("Enraged Guardian", preloadedObjects["GG_Crystal_Guardian_2"]["Battle Scene/Zombie Beam Miner Rematch"]);
            GameObjects.Add("Dung Defender", preloadedObjects["GG_Dung_Defender"]["Dung Defender"]);
            GameObjects.Add("Failed Champion", preloadedObjects["GG_Failed_Champion"]["False Knight Dream"]);
            //GameObjects.Add("False Knight", preloadedObjects["GG_False_Knight"]["Battle Control/False Knight New"]);
            GameObjects.Add("Flukemarm", preloadedObjects["GG_Flukemarm"]["Fluke Mother"]);
            GameObjects.Add("Galien", preloadedObjects["GG_Ghost_Galien"]["Warrior/Ghost Warrior Galien"]);
            GameObjects.Add("Gorb", preloadedObjects["GG_Ghost_Gorb"]["Warrior/Ghost Warrior Slug"]);
            GameObjects.Add("Elder Hu", preloadedObjects["GG_Ghost_Hu"]["Warrior/Ghost Warrior Hu"]);
            GameObjects.Add("Markoth", preloadedObjects["GG_Ghost_Markoth"]["Warrior/Ghost Warrior Markoth"]);
            GameObjects.Add("Marmu", preloadedObjects["GG_Ghost_Marmu"]["Warrior/Ghost Warrior Marmu"]);
            GameObjects.Add("No Eyes", preloadedObjects["GG_Ghost_No_Eyes"]["Warrior/Ghost Warrior No Eyes"]);
            GameObjects.Add("Xero", preloadedObjects["GG_Ghost_Xero"]["Warrior/Ghost Warrior Xero"]);
            GameObjects.Add("Lancer", preloadedObjects["GG_God_Tamer"]["Entry Object/Lancer"]);
            GameObjects.Add("Lobster", preloadedObjects["GG_God_Tamer"]["Entry Object/Lobster"]);
            GameObjects.Add("Grey Prince Zote", preloadedObjects["GG_Grey_Prince_Zote"]["Grey Prince"]);
            GameObjects.Add("Troupe Master Grimm", preloadedObjects["GG_Grimm"]["Grimm Scene/Grimm Boss"]);
            GameObjects.Add("Nightmare King Grimm", preloadedObjects["GG_Grimm_Nightmare"]["Grimm Control/Nightmare Grimm Boss"]);
            GameObjects.Add("Gruz Mother", preloadedObjects["GG_Gruz_Mother"]["_Enemies/Giant Fly"]);
            GameObjects.Add("Hive Knight", preloadedObjects["GG_Hive_Knight"]["Battle Scene/Hive Knight"]);
            GameObjects.Add("Pure Vessel", preloadedObjects["GG_Hollow_Knight"]["Battle Scene/HK Prime"]);
            GameObjects.Add("Hornet Protector", preloadedObjects["GG_Hornet_1"]["Boss Holder/Hornet Boss 1"]);
            GameObjects.Add("Hornet Sentinel", preloadedObjects["GG_Hornet_2"]["Boss Holder/Hornet Boss 2"]);
            GameObjects.Add("Lost Kin", preloadedObjects["GG_Lost_Kin"]["Lost Kin"]);
            GameObjects.Add("Pale Lurker", preloadedObjects["GG_Lurker"]["Lurker Control/Pale Lurker"]);
            GameObjects.Add("Soul Warrior", preloadedObjects["GG_Mage_Knight"]["Mage Knight"]);
            GameObjects.Add("Mantis Lord", preloadedObjects["GG_Mantis_Lords"]["Mantis Battle/Battle Main/Mantis Lord"]);       
            GameObjects.Add("Massive Moss Charger", preloadedObjects["GG_Mega_Moss_Charger"]["Mega Moss Charger"]);
            GameObjects.Add("Mato", preloadedObjects["GG_Nailmasters"]["Brothers/Mato"]);
            GameObjects.Add("Oro", preloadedObjects["GG_Nailmasters"]["Brothers/Oro"]);
            GameObjects.Add("Nosk", preloadedObjects["GG_Nosk"]["Mimic Spider"]);
            GameObjects.Add("Winged Nosk", preloadedObjects["GG_Nosk_Hornet"]["Battle Scene/Hornet Nosk"]);
            GameObjects.Add("Oblobble", preloadedObjects["GG_Oblobbles"]["Mega Fat Bee"]);
            GameObjects.Add("Paintmaster Sheo", preloadedObjects["GG_Painter"]["Battle Scene/Sheo Boss"]);
            GameObjects.Add("Absolute Radiance", preloadedObjects["GG_Radiance"]["Boss Control/Absolute Radiance"]);
            GameObjects.Add("Great Nailsage Sly", preloadedObjects["GG_Sly"]["Battle Scene/Sly Boss"]);
            GameObjects.Add("Soul Master", preloadedObjects["GG_Soul_Master"]["Mage Lord"]);
            GameObjects.Add("Soul Tyrant", preloadedObjects["GG_Soul_Tyrant"]["Dream Mage Lord"]);
            GameObjects.Add("Traitor Lord", preloadedObjects["GG_Traitor_Lord"]["Battle Scene/Wave 3/Mantis Traitor Lord"]);
            GameObjects.Add("Uumuu", preloadedObjects["GG_Uumuu"]["Mega Jellyfish GG"]);
            GameObjects.Add("Vengefly King", preloadedObjects["GG_Vengefly"]["Giant Buzzer Col"]);
            GameObjects.Add("Watcher Knight", preloadedObjects["GG_Watcher_Knights"]["Battle Control/Black Knight 1"]);
            GameObjects.Add("White Defender", preloadedObjects["GG_White_Defender"]["White Defender"]);*/

            Instance = this;

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
                GameObject.Find("Colosseum Manager").AddComponent<ColosseumManager>();
            }
        }
    }
}