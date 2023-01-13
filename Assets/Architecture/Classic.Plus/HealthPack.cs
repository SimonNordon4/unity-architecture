using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Architecture.Classic.Plus
{
    public class HealthPack : MonoBehaviour
    {
        private HealthPackSpawner _spawner;
        [SerializeField] private int _healthAmount = 1;

        public void SetSpawner(HealthPackSpawner spawner)
        {
            _spawner = spawner;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var playerHealth = other.GetComponent<Health>();
                if (playerHealth.Current < playerHealth.Max)
                {
                    playerHealth.ApplyHeal(_healthAmount);
                }

                _spawner.RemoveHealthPack();
                Destroy(gameObject);
            }
        }
    }
}

