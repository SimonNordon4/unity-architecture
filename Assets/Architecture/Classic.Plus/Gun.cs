using UnityEngine;

namespace Architecture.Classic.Plus
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Move bulletPrefab;
        [field:SerializeField]
        public float BulletCooldown { get; private set; } = 1f;
        [SerializeField]
        private float range = 5f;
        private float _timeSinceLastBullet = float.PositiveInfinity;

        private void Update()
        {
            _timeSinceLastBullet += Time.deltaTime;
            if (_timeSinceLastBullet > BulletCooldown)
            {
                // Find the closet enemy.
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

                // Only fire a bullet if an enemy was found.
                if (closestEnemy != null)
                {
                    var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    bullet.Direction = (closestEnemy.position - transform.position).normalized;
                    _timeSinceLastBullet = 0.0f;
                    _timeSinceLastBullet = 0.0f;
                }
            }
        }

        public void DecreaseCooldown(float percentageOfCurrent)
        {
            BulletCooldown *= percentageOfCurrent;
        }
    }
}