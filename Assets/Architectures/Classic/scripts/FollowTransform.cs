using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Classic
{
    public class FollowTransform : MonoBehaviour
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

