using System.Collections.Generic;
using UnityEngine;

namespace CustomTrial.Classes
{
    public class Wave
    {
        // Enemies and their spawn locations
        public Dictionary<GameObject, Vector2> Enemies = new Dictionary<GameObject, Vector2>();
        // Platforms and their spawn locations
        public Dictionary<GameObject, Vector2> Platforms = new Dictionary<GameObject, Vector2>();
        // Whether spikes should cover the bottom of the arena
        public bool Spikes;
        public float Cooldown;

        public static Wave Instance;
        
        public Wave(bool spikes, float cooldown)
        {
            Spikes = spikes;
            Cooldown = cooldown;
        }

        public void AddEnemy(GameObject enemy, Vector2 spawnPoint)
        {
            Enemies.Add(enemy, spawnPoint);
        }
    }
}