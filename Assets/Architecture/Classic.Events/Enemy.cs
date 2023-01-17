using System;
using UnityEngine;

namespace Architecture.Classic.Events
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int damage = 1;
        [SerializeField] private bool dieOnTouch = false;

        private Transform _playerTransform;
        private EnemySpawner _spawner;
        private Move _move;

        private LayerMask _playerLayer;

        private void Start()
        {
            _playerTransform = GameCatalog.Instance.Player.transform;
            _spawner = GameCatalog.Instance.EnemySpawner;
            _move = GetComponent<Move>();
            gameObject.layer = LayerMask.NameToLayer("Enemy");
            _playerLayer = LayerMask.NameToLayer("Player");
        }

        // Due to limitations of the current architecture, we must use late update to ensure the enemies
        // are tracking the most recent game state.
        public void LateUpdate()
        {
            var directionToPlayer = (_playerTransform.position - transform.position).normalized;
            _move.Direction = directionToPlayer;
        }

        // We set the layer to "Enemy" so should only collide with that layers filters.
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == _playerLayer)
            {
                other.TryGetComponent<Health>(out var health);
                if (health != null)
                    health.ApplyDamage(damage);

                if (dieOnTouch)
                {
                    Destroy(gameObject);
                }
            }
        }

        public void OnDestroy()
        {
            _spawner.EnemyDied();
        }
    }
}