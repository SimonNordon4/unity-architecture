using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Classic
{
    public class PlayerController : MonoBehaviour
    {
        private Move _move;
        private Gun _gun;

        private void OnEnable()
        {
            _move = GetComponent<Move>();
            _gun = GetComponent<Gun>();
        }

        public void Update()
        {
            // Move the PlayerController
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
            
            _move.MovePerFrame(moveDirection);
            
            // Bullet Firing
            if (Input.GetMouseButton(0))
            {
                var bulletDirection = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _gun.FireBullet(bulletDirection);
            }
        }
    }
}

