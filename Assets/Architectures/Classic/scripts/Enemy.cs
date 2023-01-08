using System;
using UnityEngine;

namespace Classic
{
    public class Enemy : MonoBehaviour
    {
        public EnemySpawner spawner;
        public Transform playerTransform;
        public int damage = 1;

        public bool dieOnTouch = false;
        
        private Health _health;
        private Move _move;

        private void Start()
        {
            _health = GetComponent<Health>();
            _move = GetComponent<Move>();
        }

        public void Update()
        {
            var directionToPlayer = (playerTransform.position - transform.position).normalized;
            _move.direction = directionToPlayer;

            if (_health.currentHealth <= 0)
            {
                spawner.EnemyDied();
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            // if the player is entered, deal damage
            if (other.CompareTag("player"))
            {
                other.GetComponent<Health>().currentHealth -= damage;

                if (dieOnTouch)
                {
                    spawner.EnemyDied();
                    Destroy(gameObject);
                }
            }
        }
    }
}