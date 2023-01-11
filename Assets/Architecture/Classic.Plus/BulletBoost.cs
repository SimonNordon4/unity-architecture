using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture.Classic.Plus
{
    /// <summary>
    /// A pickup for the player which increases the gun cooldown.
    /// </summary>
    public class BulletBoost : MonoBehaviour
    {
        public BulletBoostSpawner Spawner { get; set; }
        [field:SerializeField]
        public float CooldownReductionPercentage { get; private set; } = 0.1f;

        private void OnTriggerEnter(Collider other)
        {
            // TODO: Replace with physics layers
            if (other.gameObject.CompareTag("Player"))
            {
                // Reduce the players bullet cooldown by a percentage of its current value.
                var playerGun = other.GetComponent<Gun>();
                playerGun.bulletCooldown *= (1 - CooldownReductionPercentage);
                
                // Destroy the pickup.
                Spawner.concurrentPickups--;
                Destroy(gameObject);
            }
        }
    }
}