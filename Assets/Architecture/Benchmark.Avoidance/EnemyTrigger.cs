using System;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture.Benchmark.Avoidance
{
    public class EnemyTrigger : MonoBehaviour
    {
        public Transform playerTransform;
        private Move _move;

        private readonly List<Transform> _enemies = new List<Transform>();

        private Vector3 _repulsionDirection = Vector3.zero;
        private float _enemyRadius;

        private void Start()
        {
            _move = GetComponent<Move>();
            _enemyRadius = GetComponent<CapsuleCollider>().radius;
            playerTransform = PlayerTrigger.Instance.transform;
        }

        public void FixedUpdate()
        {
            var playerPosition = playerTransform.position;
            var currentPosition = transform.position;
            var directionToPlayer = (playerPosition - currentPosition).normalized;
            
            _repulsionDirection = Vector3.zero;
            
            foreach (var enemy in _enemies)
            {
                var enemyPosition = enemy.position;
                var directionToEnemy = (currentPosition - enemyPosition).normalized;
                var distanceToEnemy = Vector3.Distance(enemyPosition, currentPosition);
                
                // maximum distance to enemy should only every be the radius * 2. We want repulsion to increase the closer the enemy is.
                var repulsionMagnitude = _enemyRadius * 2 - distanceToEnemy;
                var repulsion = directionToEnemy * repulsionMagnitude;
                _repulsionDirection += repulsion;
            }
            _move.Direction = directionToPlayer + _repulsionDirection.normalized * EnemySpawner.Instance.repulsionMultiplier;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                _enemies.Add(other.transform);
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                _enemies.Remove(other.transform);
            }
        }
    }
}