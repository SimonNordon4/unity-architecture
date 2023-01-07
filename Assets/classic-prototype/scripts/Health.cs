using System;
using UnityEngine;

namespace Classic
{
    public class Health : MonoBehaviour
    {
        public int maxHealth = 10;
        public int currentHealth = 0;

        private void OnEnable()
        {
            currentHealth = maxHealth;
        }

        private void TakeDamage(int damageAmount)
        {
            currentHealth -= damageAmount;
            
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