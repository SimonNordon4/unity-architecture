using System;
using UnityEngine;

namespace Architecture.Classic.Events
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private float bulletLifetime = 10f;
        [SerializeField] 
        private int bulletDamage = 1;

        private LayerMask _enemyLayer;

        private void Start()
        {
            // Ensure the physics layer is attack
            gameObject.layer = LayerMask.NameToLayer("Attack");
            _enemyLayer = LayerMask.NameToLayer("Enemy");
        }

        public void Update()
        {
            bulletLifetime -= Time.deltaTime;
            if (bulletLifetime <= 0)
            {
                // TODO: Don't destroy, pool
                Destroy(gameObject);
            }
        }

        // We set the GameObject to Attack layer, so will only collide with its filters.
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == _enemyLayer)
            {
                other.GetComponent<Health>().ApplyDamage(bulletDamage);
                Destroy(gameObject);
            }
        }
    }
}