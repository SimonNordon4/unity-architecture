using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Architecture.Classic.Plus
{
    public class EnemySpawner : MonoBehaviour
    {
        // Enemy types we want to spawn.
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private Enemy smallEnemyPrefab;
        [SerializeField] private Enemy bigEnemyPrefab;

        // Probabilities of each enemy type spawning.
        [SerializeField] private float smallEnemySpawnChance = 0.1f;
        [SerializeField] private float bigEnemySpawnChance = 0.03f;

        // Maximum enemies to spawn in level.
        [field: SerializeField] public int EnemiesToSpawn { get; private set; }= 100;

        public int CurrentEnemies { get; private set; }
        [SerializeField] private int maxCurrentEnemies = 50;

        [SerializeField] private float enemySpawnRate = 1f;
        // Every time an enemy spawns, the next enemy will spawn slightly faster.
        [SerializeField] private float maxEnemySpawnRate = 0.1f;
        private float _enemySpawnRateIncrement = 0.0f;
        private float _timeSinceLastEnemySpawn = 0f;

        private Transform _playerTransform;
        [SerializeField] private Vector2 minMaxSpawnDistance = new Vector2(5f, 10f);

        private void Start()
        {
            _enemySpawnRateIncrement = (enemySpawnRate - maxEnemySpawnRate) / EnemiesToSpawn;
            _playerTransform = GameCatalog.Instance.Player.transform;
        }

        void Update()
        {
            // If the enemy spawner cooldown has been satisfied, spawn an enemy.
            _timeSinceLastEnemySpawn += Time.deltaTime;
            if (_timeSinceLastEnemySpawn >= enemySpawnRate && EnemiesToSpawn > 0)
            {
                if (CurrentEnemies < maxCurrentEnemies)
                {
                    // Create a random spawn position within the spawn area that is 1 away from the player position.
                    Vector2 randomDirection = Random.insideUnitCircle.normalized;
                    Vector3 randomWorldDirection = new Vector3(randomDirection.x, 0f, randomDirection.y);
                    var randomDistance = Random.Range(minMaxSpawnDistance.x, minMaxSpawnDistance.y);
                    Vector3 spawnPosition = randomDistance * randomWorldDirection + _playerTransform.position;

                    var enemyToSpawn = enemyPrefab;
                
                    // Roll to see if we spawn a small enemy.
                    if (RandomSpawnChance(smallEnemySpawnChance))
                        enemyToSpawn = smallEnemyPrefab;
  
                    if (RandomSpawnChance(bigEnemySpawnChance))
                        enemyToSpawn = bigEnemyPrefab;
                
                    var newEnemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
                    newEnemy.spawner = this;
                    newEnemy.playerTransform = _playerTransform;
                
                    CurrentEnemies++;
                    EnemiesToSpawn--;
                
                    // Increase the spawn rate of the next enemy.
                    enemySpawnRate -= _enemySpawnRateIncrement;
                }
                _timeSinceLastEnemySpawn = 0f;
            }
        }

        private bool RandomSpawnChance(float chance)
        {
            return Random.Range(0, 100) < chance * 100f;
        }
        
        public void EnemyDied()
        {
            CurrentEnemies--;
        }
    }
}