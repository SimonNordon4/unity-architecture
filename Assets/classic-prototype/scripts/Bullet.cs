using System;
using UnityEngine;

namespace Classic
{
    public class Bullet : MonoBehaviour
    {
        public Vector3 direction;
        public int damage = 1;
        public float speed = 5f;

        public float bulletLifetime = 20f;

        public void Update()
        {
            transform.Translate(direction * (speed * Time.deltaTime));
        }

        public void OnTriggerEnter(Collider other)
        {
            // if (other.gameObject.tag == "Enemy")
            // {
            //     other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            //     Destroy(this);
            // }            
        }
    }
}