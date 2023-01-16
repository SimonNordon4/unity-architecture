using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Architecture.Benchmark.Avoidance
{
    public class EnemySpawner : MonoBehaviour
    {
        // Enemy types we want to spawn.
        [SerializeField] private Enemy enemyPrefab;

        // Maximum enemies to spawn in level.
        [field: SerializeField] public int EnemiesToSpawn { get; private set; } = 100;

        [SerializeField] private float enemySpawnRate = 1f;

        // Every time an enemy spawns, the next enemy will spawn slightly faster.
        private float _timeSinceLastEnemySpawn = 0f;

        [FormerlySerializedAs("_playerTransform")] [SerializeField] private Transform playerTransform;
        [SerializeField] private Vector2 minMaxSpawnDistance = new Vector2(5f, 10f);


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
                newEnemy.spawner = this;
                newEnemy.playerTransform = playerTransform;

                EnemiesToSpawn--;
                _timeSinceLastEnemySpawn = 0f;
            }
        }
    }
}