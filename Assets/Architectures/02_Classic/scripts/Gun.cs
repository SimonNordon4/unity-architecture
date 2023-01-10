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

        private void Update()
        {
            _timeSinceLastBullet += Time.deltaTime;
            if (_timeSinceLastBullet > bulletCooldown)
            {
                FireBullet();
                _timeSinceLastBullet = 0.0f;
            }
        }

        public void FireBullet()
        {
            // find the closet enemy.
            var results = new Collider[128];
            var enemies = Physics.OverlapSphereNonAlloc(transform.position,
                range,
                results,
                Physics.AllLayers,
                QueryTriggerInteraction.Collide);

            Transform closestEnemy = null;
            float closestEnemyDistance = float.PositiveInfinity;
            
            for(int i = 0; i < enemies; i++)
            {
                if (results[i].gameObject.CompareTag("Enemy"))
                {
                    var distance = Vector3.Distance(transform.position, results[i].transform.position);
                    
                    if (distance < closestEnemyDistance)
                    {
                        closestEnemy = results[i].transform;
                        closestEnemyDistance = distance;
                    }
                }
            }

            if (closestEnemy == null)
                return;
            
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Move>().direction = (closestEnemy.position - transform.position).normalized;
            _timeSinceLastBullet = 0.0f;
     
        }
    }
}