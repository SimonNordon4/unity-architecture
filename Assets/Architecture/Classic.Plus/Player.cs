using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture.Classic.Plus
{
    [RequireComponent(typeof(Move))]
    public class Player : MonoBehaviour
    {
        private Move _move;

        private void Start()
        {
            _move = GetComponent<Move>();
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

        // Using the inbuilt on destroy is the only way we can decouple the players health
        // from their interface with the current Architecture.
        private void OnDestroy()
        {
            GameCatalog.Instance.GameInterface.LoseGame();  
        }
    }
}

