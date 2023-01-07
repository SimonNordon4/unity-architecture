using UnityEngine;
using Random = UnityEngine.Random;

namespace Classic
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public GameObject smallEnemyPrefab;
        public GameObject bigEnemyPrefab;

        public float smallEnemySpawnChance = 0.1f;
        public float bigEnemySpawnChance = 0.03f;

        public int enemiesToSpawn = 100;

        public int currentEnemies = 0;
        public int maxCurrentEnemies = 50;

        public float enemySpawnRate = 1f;
        public float enemySpawnRateAcceleration = 0.05f;
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
                
                // decrease the spawn rate. 
                enemySpawnRate *= 1-enemySpawnRateAcceleration;
            }
        }

        public void EnemyDied()
        {
            currentEnemies--;
        }
    }
}