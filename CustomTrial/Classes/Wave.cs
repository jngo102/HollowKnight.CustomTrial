using System;
using System.Collections.Generic;
using UnityEngine;

namespace CustomTrial.Classes
{
    [Serializable]
    public class BattleControl
    {
        public List<Wave> Waves;

        public BattleControl()
        {
            Waves = new List<Wave>();
        }

        public BattleControl(List<Wave> waves)
        {
            Waves = new List<Wave>();
            foreach (Wave wave in waves)
            {
                Waves.Add(wave);
            }
        }
    }
    
    [Serializable]
    public class Wave
    {
        public List<string> Enemies;
        public List<Vector2> EnemySpawn;
        public bool Spikes;
        public List<Vector2> PlatformSpawn;
        public float Cooldown;
        public float WallCDistance;
        public float WallLDistance;
        public float WallRDistance;

        public Wave()
        {
            Enemies = new List<string>();
            EnemySpawn = new List<Vector2>();
            Spikes = false;
            PlatformSpawn = new List<Vector2>();
            Cooldown = 0.0f;
            WallCDistance = 0.0f;
            WallLDistance = 0.0f;
            WallRDistance = 0.0f;
        }
        
        public Wave(List<string> enemies, List<Vector2> enemySpawn, bool spikes, List<Vector2> platSpawn, float cooldown, float wallCDistance, float wallLDistance, float wallRDistance)
        {
            Enemies = new List<string>();
            foreach (string enemyName in enemies)
            {
                Enemies.Add(enemyName);
            }
            
            EnemySpawn = new List<Vector2>();
            foreach (Vector2 spawnPos in enemySpawn)
            {
                EnemySpawn.Add(spawnPos);
            }
            
            Spikes = spikes;
            
            PlatformSpawn = new List<Vector2>();
            foreach (Vector2 platPos in platSpawn)
            {
                PlatformSpawn.Add(platPos);
            }
            
            Cooldown = cooldown;
            WallCDistance = wallCDistance;
            WallLDistance = wallLDistance;
            WallRDistance = wallRDistance;
        }
    }
}