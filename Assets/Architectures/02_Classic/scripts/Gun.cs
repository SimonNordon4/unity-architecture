using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Classic
{
    public class Gun : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public float bulletCooldown = 1f;
        public float range = 5f;
        private float _timeSinceLastBullet = float.PositiveInfinity;

        private void Start()
        {
            
        }

        private void Update()
        {
            _timeSinceLastBullet += Time.deltaTime;
        }

        public void FireBullet(Vector3 direction)
        {
            // find the closet enemy.
            var results = new Collider[100];
            var enemies = Physics.OverlapSphereNonAlloc(transform.position,
                range,
                results);

            Debug.Log("enemies size: " + enemies);
            
            if (_timeSinceLastBullet > bulletCooldown)
            {
                var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<Move>().direction = direction;
                _timeSinceLastBullet = 0.0f;
            }
        }
    }
}