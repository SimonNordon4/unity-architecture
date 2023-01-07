using UnityEngine;

namespace Classic
{
    public class Move : MonoBehaviour
    {
        public float MoveSpeed;
        
        public void MovePerFrame(Vector3 direction)
        {
            transform.Translate(direction * MoveSpeed * Time.deltaTime);
        }
    }
}