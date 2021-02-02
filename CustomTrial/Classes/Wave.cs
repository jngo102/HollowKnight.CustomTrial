using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace CustomTrial.Classes
{
    [XmlRoot("BattleControl")]
    public class BattleControl
    {
        [XmlElement("Wave")] public List<Wave> Waves;

        public BattleControl()
        {
            Waves = new List<Wave>();
        }

        public void AddWave(Wave wave)
        {
            Waves.Add(wave);
        }
    }

    public class Wave
    {
        [XmlElement("Enemy")] public List<Enemy> Enemies;
        [XmlElement("PlatformSpawn")] public List<Vector2> PlatformSpawn;
        [XmlElement("CrowdAction")] public string CrowdAction;
        [XmlElement("MusicLevel")] public string MusicLevel;
        [XmlElement("Cooldown")] public float Cooldown;
        [XmlElement("DelayBetweenSpawns")] public float DelayBetweenSpawns;
        [XmlElement("WallCeilingDistance")] public float WallCDistance;
        [XmlElement("WallLeftDistance")] public float WallLDistance;
        [XmlElement("WallRightDistance")] public float WallRDistance;
        [XmlElement("Spikes")] public bool Spikes;

        public Wave()
        {
            Enemies = new List<Enemy>();
            PlatformSpawn = new List<Vector2>();
            CrowdAction = "";
            MusicLevel = "SILENT";
            Cooldown = 0.0f;
            DelayBetweenSpawns = 0.0f;
            WallCDistance = 0.0f;
            WallLDistance = 0.0f;
            WallRDistance = 0.0f;
            Spikes = false;
        }

        public Wave(
            List<Enemy> enemies,
            List<Vector2> platSpawn, 
            string crowdAction,
            string musicLevel,
            float cooldown,
            float delayBetweenSpawns,
            float wallCDistance, 
            float wallLDistance, 
            float wallRDistance,
            bool spikes)
        {
            Enemies = new List<Enemy>();
            foreach (Enemy enemy in enemies)
            {
                Enemies.Add(enemy);
            }

            PlatformSpawn = new List<Vector2>();
            foreach (Vector2 platPos in platSpawn)
            {
                PlatformSpawn.Add(platPos);
            }

            CrowdAction = crowdAction;
            MusicLevel = musicLevel;

            Cooldown = cooldown;
            DelayBetweenSpawns = delayBetweenSpawns;
            WallCDistance = wallCDistance;
            WallLDistance = wallLDistance;
            WallRDistance = wallRDistance;

            Spikes = spikes;
        }
    }

    public class Enemy
    {
        [XmlElement("Name")] public string Name;
        [XmlElement("Health")] public int Health;
        [XmlElement("SpawnPosition")] public Vector2 SpawnPosition;

        public Enemy()
        {
            Name = "";
            Health = 0;
            SpawnPosition = Vector2.zero;
        }

        public Enemy(string name, int health, Vector2 spawnPosition)
        {
            Name = name;
            Health = health;
            SpawnPosition = spawnPosition;
        }
    }
}