using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Architecture.Classic.Plus
{
    public class HealthPack : MonoBehaviour
    {
        private HealthPackSpawner _spawner;
        [SerializeField] private int _healthAmount = 1;

        private void Start()
        {
            gameObject.layer = LayerMask.NameToLayer("Pickup");
        }

        public void SetSpawner(HealthPackSpawner spawner)
        {
            _spawner = spawner;
        }
        
        // We set the layer to pickup, so will only collide with the layers filters.
        private void OnTriggerEnter(Collider other)
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

