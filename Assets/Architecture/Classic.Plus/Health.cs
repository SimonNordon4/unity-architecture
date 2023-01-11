using System;
using UnityEngine;

namespace Architecture.Classic.Plus
{
    public class Health : MonoBehaviour
    {
        public int maxHealth = 10;
        public int currentHealth = 0;

        private void Start()
        {
            currentHealth = maxHealth;
        }
    }
}