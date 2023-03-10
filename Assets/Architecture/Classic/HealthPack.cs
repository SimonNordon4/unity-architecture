using System;
using UnityEngine;

namespace Architecture.Classic
{
    public class HealthPack : MonoBehaviour
    {
        public HealthPackSpawner spawner;
        public int healthAmount = 1;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var playerHealth = other.GetComponent<Health>();
                if (playerHealth.currentHealth < playerHealth.maxHealth)
                {
                    playerHealth.currentHealth += healthAmount;
                }

                spawner.concurrentPickups--;
                Destroy(gameObject);
            }
        }
    }
}

