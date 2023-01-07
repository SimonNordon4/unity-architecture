using UnityEngine;
using Random = UnityEngine.Random;

namespace Classic
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;

        public int enemiesToSpawn = 100;

        public int currentEnemies = 0;
        public int maxCurrentEnemies = 15;

        public float enemySpawnRate = 1f;
        private float _timeSinceLastEnemySpawn = 0f;

        public Transform playerTransform;
        public Vector2 minMaxSpawnDistance = new Vector2(5f, 10f);

        // Update is called once per frame
        void Update()
        {
            _timeSinceLastEnemySpawn += Time.deltaTime;
            if (_timeSinceLastEnemySpawn >= enemySpawnRate && enemiesToSpawn > 0)
            {
                SpawnEnemy();
                _timeSinceLastEnemySpawn = 0f;
            }
        }

        public void SpawnEnemy()
        {
            if (currentEnemies < maxCurrentEnemies)
            {
                // create a random spawn position within the spawn area that is 1 away from the player position
                Vector2 randomDirection = Random.insideUnitCircle;
                Vector3 randomWorldDirection = new Vector3(randomDirection.x, 0f, randomDirection.y);
                Vector3 spawnPosition =
                    randomWorldDirection * Random.Range(minMaxSpawnDistance.x, minMaxSpawnDistance.y) +
                    playerTransform.position;
                spawnPosition.y = 1f;

                var newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                var newEnemyController = newEnemy.GetComponent<Enemy>();
                newEnemyController.spawner = this;
                newEnemyController.playerTransform = playerTransform;
                
                currentEnemies++;
                enemiesToSpawn--;
            }
        }

        public void EnemyDied()
        {
            currentEnemies--;
        }
    }
}