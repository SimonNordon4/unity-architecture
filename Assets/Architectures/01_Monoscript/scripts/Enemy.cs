using System;
using UnityEngine;

namespace MonoScript
{
    public class Enemy : MonoBehaviour
    {
        public GameController gameController;

        public int maxHealth;
        public int currentHealth;
        public float moveSpeed;
        public int damage;

        public void OnTriggerEnter(Collider other)
        {
            gameController.EnemyHit(this, other);
        }
    }
}