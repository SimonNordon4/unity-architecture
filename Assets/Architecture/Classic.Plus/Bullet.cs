using System;
using UnityEngine;

namespace Architecture.Classic.Plus
{
    public class Bullet : MonoBehaviour
    {
        [field:SerializeField]
        public float BulletLifetime { get; private set; } = 10f;
        [field:SerializeField]
        public int BulletDamage { get; private set; } = 1;

        private void Start()
        {
            // Ensure the physics layer is attack
            gameObject.layer = LayerMask.NameToLayer("Attack");
        }

        public void Update()
        {
            BulletLifetime -= Time.deltaTime;
            if (BulletLifetime <= 0)
            {
                // TODO: Don't destroy, pool
                Destroy(gameObject);
            }
        }

        // We set the GameObject to Attack layer, so will only collide with its filters.
        public void OnTriggerEnter(Collider other)
        {
            other.GetComponent<Health>().ApplyDamage(BulletDamage);
            Destroy(gameObject);
        }
    }
}