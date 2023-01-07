using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Classic
{
    public class Gun : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public float bulletCooldown = 1f;
        private float _timeSinceLastBullet = float.PositiveInfinity;

        private void Update()
        {
            _timeSinceLastBullet += Time.deltaTime;
        }

        public void FireBullet(Vector3 direction)
        {
            if (_timeSinceLastBullet > bulletCooldown)
            {
                var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().direction = direction;
                _timeSinceLastBullet = 0.0f;
            }
        }
    }
}