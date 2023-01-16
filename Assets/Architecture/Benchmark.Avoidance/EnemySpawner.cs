using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Architecture.Benchmark.Avoidance
{
    public class EnemySpawner : MonoBehaviour
    {
        public static EnemySpawner Instance { get; private set; }
        // Enemy types we want to spawn.
        [SerializeField] private GameObject enemyPrefab;

        // Maximum enemies to spawn in level.
        [field: SerializeField] public int EnemiesToSpawn { get; private set; } = 100;
        [SerializeField] private float enemySpawnRate = 1f;

        public float repulsionMultiplier = 1.0f;

        // Every time an enemy spawns, the next enemy will spawn slightly faster.
        private float _timeSinceLastEnemySpawn = 0f;

        [SerializeField] private Transform playerTransform;
        [SerializeField] private Vector2 minMaxSpawnDistance = new Vector2(5f, 10f);


        private void Start()
        {
            Instance = this;
        }

        void Update()
        {
            // If the enemy spawner cooldown has been satisfied, spawn an enemy.
            _timeSinceLastEnemySpawn += Time.deltaTime;
            if (_timeSinceLastEnemySpawn >= enemySpawnRate && EnemiesToSpawn > 0)
            {
                // Create a random spawn position within the spawn area that is 1 away from the player position.
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                Vector3 randomWorldDirection = new Vector3(randomDirection.x, 0f, randomDirection.y);
                var randomDistance = Random.Range(minMaxSpawnDistance.x, minMaxSpawnDistance.y);
                Vector3 spawnPosition = randomDistance * randomWorldDirection + playerTransform.position;

                var enemyToSpawn = enemyPrefab;

                var newEnemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
                EnemiesToSpawn--;
                _timeSinceLastEnemySpawn = 0f;
            }
        }
    }
}