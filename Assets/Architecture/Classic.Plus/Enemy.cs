using System;
using UnityEngine;

namespace Architecture.Classic.Plus
{
    public class Enemy : MonoBehaviour
    {
        // change to singleton
        public EnemySpawner spawner;
        
        // change to singleton
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
            _move.Direction = directionToPlayer;

            if (_health.currentHealth <= 0)
            {
                
            }
        }
        
        public void ApplyDamage(int amount)
        {
            _health.currentHealth -= amount;
            if( _health.currentHealth <=0 )
            {
                Destroy(this);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            // if the player is entered, deal damage
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Player>().ApplyDamage(damage);

                if (dieOnTouch)
                {
                    spawner.currentEnemies--;
                    Destroy(gameObject);
                }
            }
        }
    }
}