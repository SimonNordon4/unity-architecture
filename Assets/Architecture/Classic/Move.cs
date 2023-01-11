using System;
using UnityEngine;

namespace Architecture.Classic
{
    public class Move : MonoBehaviour
    {
        public float moveSpeed;
        public Vector3 direction;

        public void Update()
        {       
            transform.Translate(direction * (moveSpeed * Time.deltaTime));
        }
    }
}