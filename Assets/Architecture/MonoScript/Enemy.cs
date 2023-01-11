using System;
using UnityEngine;

namespace Architectures.MonoScript
{
    /// Simple Class that contains data, and informs the GameController OnTriggerEnter
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