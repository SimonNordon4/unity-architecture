using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture.Classic
{
    /// <summary>
    /// A pickup for the player which increases the gun cooldown.
    /// </summary>
    public class BulletBoost : MonoBehaviour
    {
        public BulletBoostSpawner spawner;
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