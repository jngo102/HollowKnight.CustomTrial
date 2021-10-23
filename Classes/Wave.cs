using System;
using System.Collections.Generic;
using UnityEngine;

namespace CustomTrial.Classes
{
    [Serializable]
    public class Wave
    {
        public List<Enemy> Enemies;
        public List<Vector2> PlatformSpawn;
        public string CrowdAction;
        public string MusicLevel;
        public float Cooldown;
        public float DelayBetweenSpawns;
        public float WallCDistance;
        public float WallLDistance;
        public float WallRDistance;
        public bool Spikes;

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

    [Serializable]
    public class Enemy
    {
        public string Name;
        public int Health;
        public Vector2 SpawnPosition;

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