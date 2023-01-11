using System;
using UnityEngine;

namespace Architecture.Classic
{
    public class Bullet : MonoBehaviour
    {
        public float bulletLifetime = 10f;
        public int bulletDamage = 1;

        public void Update()
        {
            bulletLifetime -= Time.deltaTime;
            if (bulletLifetime <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.GetComponent<Health>().currentHealth -= bulletDamage;
                Destroy(gameObject);
            }
        }
    }
}