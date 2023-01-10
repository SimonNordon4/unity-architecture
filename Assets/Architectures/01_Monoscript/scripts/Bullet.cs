using System;
using UnityEngine;

namespace MonoScript
{
    public class Bullet : MonoBehaviour
    {
        public GameController gameController;
        public Vector3 direction;
        public float timeAlive = 0.0f;
        public void OnTriggerEnter(Collider other)
        {
            gameController.BulletHit(this, other);
        }
    }
}