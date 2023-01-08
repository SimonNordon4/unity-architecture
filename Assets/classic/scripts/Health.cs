using System;
using UnityEngine;

namespace Classic
{
    public class Health : MonoBehaviour
    {
        public int maxHealth = 10;
        public int currentHealth = 0;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        private void Update()
        {
            if(currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}