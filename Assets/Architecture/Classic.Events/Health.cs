using System;
using UnityEngine;

namespace Architecture.Classic.Events
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

        // This is a bad way to handle death, it would be much better to use an Event (Coming Soon)
        private void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}