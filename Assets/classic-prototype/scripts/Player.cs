using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Classic
{
    public class Player : MonoBehaviour
    {
        private Move _move;
        private Gun _gun;

        private void Start()
        {
            _move = GetComponent<Move>();
            _gun = GetComponent<Gun>();
        }

        public void Update()
        {
            // Move the Player
            var moveDirection = Vector3.zero;
            if (Input.GetKey(KeyCode.D))
            {
                moveDirection.x += 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveDirection.x -= 1;
            }
            if (Input.GetKey(KeyCode.W))
            {
                moveDirection.z += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveDirection.z -= 1;
            }

            _move.direction = moveDirection;
            
            // Bullet Firing
            if (Input.GetMouseButton(0))
            {
                // get the hit position of a raycast from the mouse position
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out var hit))
                {
                    var bulletDirection = hit.point - transform.position;
                    bulletDirection.y = 0;
                    _gun.FireBullet(bulletDirection.normalized);
                }
            }
        }
    }
}

