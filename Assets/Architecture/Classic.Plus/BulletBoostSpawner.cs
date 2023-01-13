using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Architecture.Classic.Plus
{
    /// <summary>
    /// Spawns bullet boosts at set intervals.
    /// </summary>
    public class BulletBoostSpawner : MonoBehaviour
    {
        // Instantiate the component directly.
        [field: SerializeField]
        public BulletBoost PickupPrefab { get; private set; }
        
        // TODO: replace with a singleton.
        public Transform playerTransform;

        public int maxConcurrentPickups = 3;
        public int concurrentPickups = 0;
        
        public float spawnInterval = 5f;
        private float _timeSinceLastSpawn = 0f;
        
        public Vector2 minMaxSpawnDistance = new Vector2(10f, 30f);

        private void Start()
        {
            playerTransform = GameCatalog.Instance.Player.transform;
        }

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
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            if (concurrentPickups < maxConcurrentPickups)
            {
                concurrentPickups++;
                
                // Create a random spawn position within the spawn area that is 1 away from the player position.
                var randomWorldDirection = new Vector3(randomDirection.x, 0f, randomDirection.y);
                var spawnPosition = randomWorldDirection
                                    * Random.Range(minMaxSpawnDistance.x, minMaxSpawnDistance.y) 
                                    + playerTransform.position;
                spawnPosition.y = 0.5f;
                
                var bulletBoost = Instantiate(PickupPrefab, spawnPosition, Quaternion.identity);
                bulletBoost.Spawner = this;
            }
        }
    }
    
    
}

