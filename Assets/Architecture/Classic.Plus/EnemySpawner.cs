using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Architecture.Classic.Plus
{
    public class EnemySpawner : MonoBehaviour
    {
        // Enemy types we want to spawn.
        public GameObject enemyPrefab;
        public GameObject smallEnemyPrefab;
        public GameObject bigEnemyPrefab;

        // Probabilities of each enemy type spawning.
        public float smallEnemySpawnChance = 0.1f;
        public float bigEnemySpawnChance = 0.03f;

        // Maximum enemies to spawn in level.
        public int enemiesToSpawn = 100;

        public int currentEnemies = 0;
        public int maxCurrentEnemies = 50;

        public float enemySpawnRate = 1f;
        // Every time an enemy spawns, the next enemy will spawn slightly faster.
        public float maxEnemySpawnRate = 0.1f;
        private float _enemySpawnRateIncrement = 0.0f;
        private float _timeSinceLastEnemySpawn = 0f;

        public Transform playerTransform;
        public Vector2 minMaxSpawnDistance = new Vector2(5f, 10f);

        private void Start()
        {
            // Initialise by calculating the decrement in spawn rate per enemy spawn.
            _enemySpawnRateIncrement = (enemySpawnRate - maxEnemySpawnRate) / enemiesToSpawn;
        }

        void Update()
        {
            // If the enemy spawner cooldown has been satisfied, spawn an enemy.
            _timeSinceLastEnemySpawn += Time.deltaTime;
            if (_timeSinceLastEnemySpawn >= enemySpawnRate && enemiesToSpawn > 0)
            {
                if (currentEnemies < maxCurrentEnemies)
                {
                    // Create a random spawn position within the spawn area that is 1 away from the player position.
                    Vector2 randomDirection = Random.insideUnitCircle.normalized;
                    Vector3 randomWorldDirection = new Vector3(randomDirection.x, 0f, randomDirection.y);
                    var randomDistance = Random.Range(minMaxSpawnDistance.x, minMaxSpawnDistance.y);
                    Vector3 spawnPosition = randomDistance * randomWorldDirection + playerTransform.position;

                    var enemyToSpawn = enemyPrefab;
                
                    // roll to see if we spawn a small enemy.
                    var spawnSmallEnemy = Random.Range(0,100) < smallEnemySpawnChance * 100f;
                    if (spawnSmallEnemy)
                    {
                        enemyToSpawn = smallEnemyPrefab;
                    }

                    // roll to see if we spawn a big enemy.
                    var spawnBigEnemy = Random.Range(0,100) < bigEnemySpawnChance *100f;
                    if (spawnBigEnemy)
                        enemyToSpawn = bigEnemyPrefab;
                
                    var newEnemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
                    var newEnemyController = newEnemy.GetComponent<Enemy>();
                    newEnemyController.spawner = this;
                    newEnemyController.playerTransform = playerTransform;
                
                    currentEnemies++;
                    enemiesToSpawn--;
                
                    // Increase the spawn rate of the next enemy.
                    enemySpawnRate -= _enemySpawnRateIncrement;
                }
                _timeSinceLastEnemySpawn = 0f;
            }
        }
    }
}