using System;
using UnityEngine;

namespace Classic
{
    public class HealthPack : MonoBehaviour
    {
        public PickupSpawner spawner;
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

