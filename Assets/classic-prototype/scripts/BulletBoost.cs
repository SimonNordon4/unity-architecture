using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Classic
{
    public class BulletBoost : MonoBehaviour
    {
        public PickupSpawner spawner;
        public float cooldownReductionPercentage = 0.1f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // Reduce the players bullet cooldown by a percentage of its current value.
                var playerGun = other.GetComponent<Gun>();
                playerGun.bulletCooldown *= (1 - cooldownReductionPercentage);
                
                // Destroy the pickup.
                spawner.concurrentPickups--;
                Destroy(gameObject);
            }
        }
    }
}