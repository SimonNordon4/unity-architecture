using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture.Classic.Events
{
    /// <summary>
    /// A pickup for the player which increases the gun cooldown.
    /// </summary>
    public class BulletBoost : MonoBehaviour
    {
        private BulletBoostSpawner _spawner;
        [field:SerializeField]
        public float CooldownReductionPercentage { get; private set; } = 0.1f;

        public void SetSpawner(BulletBoostSpawner spawner)
        {
            _spawner = spawner;
        }
        private void OnTriggerEnter(Collider other)
        {
            // TODO: Replace with physics layers
            if (other.gameObject.CompareTag("Player"))
            {
                // Reduce the players bullet cooldown by a percentage of its current value.
                var playerGun = other.GetComponent<Gun>();
                playerGun.DecreaseCooldown(1 - CooldownReductionPercentage);
                
                // Destroy the pickup.
                _spawner.RemoveBulletBoost();
                Destroy(gameObject);
            }
        }
    }
}