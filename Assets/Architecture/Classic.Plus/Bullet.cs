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

        public void Update()
        {
            BulletLifetime -= Time.deltaTime;
            if (BulletLifetime <= 0)
            {
                // TODO: Don't destroy, pool
                Destroy(gameObject);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            // TODO: Replace with physics tags so it always collides with a suitable object.
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.GetComponent<Health>().currentHealth -= BulletDamage;
                Destroy(gameObject);
            }
        }
    }
}