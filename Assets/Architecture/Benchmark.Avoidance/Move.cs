using System;
using UnityEngine;

namespace Architecture.Benchmark.Avoidance
{
    public class Move : MonoBehaviour
    {
        [field: SerializeField] public float MoveSpeed { get; set; } = 5f;
        public Vector3 Direction { get; set; }

        public void Update()
        {
            transform.Translate(Direction * (MoveSpeed * Time.deltaTime));
        }
    }
}