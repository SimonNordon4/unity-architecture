using System;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture.Benchmark.Avoidance
{
    public class Enemy : MonoBehaviour
    {
        // change to singleton
        public EnemySpawner spawner;
        // change to singleton
        public Transform playerTransform;
        private Move _move;

        private List<Transform> _enemies = new List<Transform>();

        private Vector3 repulsionVector;
        private float repulsionMagnitude;

        private void Start()
        {
            _move = GetComponent<Move>();
        }



        public void LateUpdate()
        {
            var directionToPlayer = (playerTransform.position - transform.position).normalized;
            
            repulsionVector = Vector3.zero;
            repulsionMagnitude = 0;
            foreach (var enemy in _enemies)
            {
                repulsionVector += (enemy.transform.position - transform.position);
            }
            
            _move.Direction = directionToPlayer + repulsionVector;
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