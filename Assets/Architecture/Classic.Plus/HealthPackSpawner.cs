using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Architecture.Classic.Plus
{
    public class HealthPackSpawner : MonoBehaviour
    {
        [SerializeField]private HealthPack pickupPrefab;
        [SerializeField]private Transform playerTransform;

        [SerializeField]private int maxConcurrentPickups = 3;
        private int _concurrentPickups = 0;
        
        [SerializeField]private float spawnInterval = 5f;
        private float _timeSinceLastSpawn = 0f;
        
        [SerializeField]private Vector2 minMaxSpawnDistance = new Vector2(10f, 30f);

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
            if (_concurrentPickups < maxConcurrentPickups)
            {
                _concurrentPickups++;
                
                // create a random spawn position within the spawn area that is 1 away from the player position
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                Vector3 randomWorldDirection = new Vector3(randomDirection.x, 0f, randomDirection.y);
                Vector3 spawnPosition =
                    randomWorldDirection * Random.Range(minMaxSpawnDistance.x, minMaxSpawnDistance.y) +
                    playerTransform.position;
                spawnPosition.y = 0.5f;
                
                var healthPack = Instantiate(pickupPrefab, spawnPosition, Quaternion.identity);
                healthPack.SetSpawner(this);
            }
        }

        public void RemoveHealthPack()
        {
            _concurrentPickups--;
        }
    }
    
    
}

