using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture.Classic.Plus
{
    [RequireComponent(typeof(Move))]
    [RequireComponent(typeof(Health))]
    public class Player : MonoBehaviour
    {
        private Move _move;
        private Health _health;

        private void Start()
        {
            _move = GetComponent<Move>();
            _health = GetComponent<Health>();
        }

        public void Update()
        {
            // Move the player
            var moveDirection = Vector3.zero;
            if (Input.GetKey(KeyCode.D))
                moveDirection.x += 1;

            if (Input.GetKey(KeyCode.A))
                moveDirection.x -= 1;

            if (Input.GetKey(KeyCode.W))
                moveDirection.z += 1;

            if (Input.GetKey(KeyCode.S))
                moveDirection.z -= 1;

            _move.Direction = moveDirection.normalized;
        }

        public void ApplyDamage(int amount)
        {
            _health.currentHealth -= amount;
            if( _health.currentHealth <=0 )
            {
                Die();
            }
        }

        private void Die()
        {
            // When the player dies, the game is lost.
            GameManager.Instance.LoseGame();
        }
    }
}

