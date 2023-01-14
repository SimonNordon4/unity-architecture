using System;
using UnityEngine;

namespace Architecture.Classic.Plus
{
    public class Enemy : MonoBehaviour
    {
        // change to singleton
        public EnemySpawner spawner;

        // change to singleton
        public Transform playerTransform;
        public int damage = 1;

        public bool dieOnTouch = false;

        private Move _move;

        private void Start()
        {
            _move = GetComponent<Move>();
            gameObject.layer = LayerMask.NameToLayer("Enemy");
        }

        // Due to limitations of the current architecture, we must use late update to ensure the enemies
        // are tracking the most recent game state.
        public void LateUpdate()
        {
            var directionToPlayer = (playerTransform.position - transform.position).normalized;
            _move.Direction = directionToPlayer;
        }

        // We set the layer to "Enemy" so should only collide with that layers filters.
        public void OnTriggerEnter(Collider other)
        {
            other.GetComponent<Health>().ApplyDamage(damage);

            if (dieOnTouch)
            {
                Destroy(gameObject);
            }
        }

        public void OnDestroy()
        {
            spawner.EnemyDied();
        }
    }
}