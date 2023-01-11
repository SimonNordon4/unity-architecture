using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture.Classic
{
    /// <summary>
    /// Follows a target based off of a starting position and a target position.
    /// </summary>
    public class FollowPosition : MonoBehaviour
    {
        public Transform target;
        private Vector3 offset;

        void Start()
        {
            offset = transform.position;
        }

        void Update()
        {
            transform.position = target.position + offset;
        }
    }
}

