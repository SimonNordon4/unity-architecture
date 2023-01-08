using System;
using UnityEngine;

namespace GodObject
{
    public class Bullet : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            God.Instance.BulletHit(this, other);
        }
    }
}