using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture.Classic.Plus
{
    /// <summary>
    /// Follows a target based off of a starting position and a target position.
    /// </summary>
    public class FollowPosition : MonoBehaviour
    {
        [SerializeField]
        private Transform target;
        private Vector3 _offset;

        void Start()
        {
            _offset = transform.position;
        }

        void LateUpdate()
        {
            transform.position = target.position + _offset;
        }
    }
}

