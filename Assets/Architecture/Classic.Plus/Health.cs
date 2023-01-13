using System;
using UnityEngine;

namespace Architecture.Classic.Plus
{
    public class Health : MonoBehaviour
    {
        [field:SerializeField]
        public int Max { get; private set; } = 10;
        public int Current { get; private set; } = 0;

        private void Start()
        {
            Current = Max;
        }
        
        public void ApplyDamage(int damage)
        {
            Current -= damage;
            if (Current <= 0)
                OnDeath();
        }

        public void ApplyHeal(int amount)
        {
            Current += amount;
            if (amount > Max)
                Current = Max;
        }

        private void OnDeath()
        {
            Debug.Log(gameObject.name + " died");
        }
    }
}