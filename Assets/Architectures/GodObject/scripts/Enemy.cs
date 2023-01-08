using System;
using UnityEngine;

namespace GodObject
{
    public class Enemy : MonoBehaviour
    {
        public God.EnemyObject Parent;
        public void OnTriggerEnter(Collider other)
        {
            God.Instance.EnemyHit(this, other);
        }
    }
}