using UnityEngine;

namespace Classic
{
    public class PickupSpawner : MonoBehaviour
    {
        public GameObject pickupPrefab;
        public Transform playerTransform;

        public int maxConcurrentPickups = 3;
        public int concurrentPickups = 0;
        
        public float spawnInterval = 5f;
        private float _timeSinceLastSpawn = 0f;
        
        Vector2 minMaxSpawnDistance = new Vector2(10f, 30f);

        private void Update()
        {
            _timeSinceLastSpawn += Time.deltaTime;
            if (_timeSinceLastSpawn >= spawnInterval)
            {
                _timeSinceLastSpawn = 0f;
                SpawnPickup();
            }
        }

        private void SpawnPickup()
        {
            if (concurrentPickups < maxConcurrentPickups)
            {
                concurrentPickups++;
                
                // create a random spawn position within the spawn area that is 1 away from the player position
                Vector2 randomDirection = Random.insideUnitCircle;
                Vector3 randomWorldDirection = new Vector3(randomDirection.x, 0f, randomDirection.y);
                Vector3 spawnPosition =
                    randomWorldDirection * Random.Range(minMaxSpawnDistance.x, minMaxSpawnDistance.y) +
                    playerTransform.position;
                spawnPosition.y = 0.5f;
                
                Instantiate(pickupPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
    
    
}

