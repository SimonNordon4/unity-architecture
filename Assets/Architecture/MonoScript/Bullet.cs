using Architectures.MonoScript;
using UnityEngine;

namespace Architecture.MonoScript
{
    /// Simple Class that contains per enemy data, and informs the GameController On Collision.
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