using System;
using TMPro;
using UnityEngine;

namespace Classic
{
    public class HealthBar : MonoBehaviour
    {
        private Health _health;

        public RectTransform healthBar;
        public TextMeshProUGUI healthText;
       
        private void Start()
        {
            _health = GetComponentInParent<Health>();
        }

        private void Update()
        {
            healthBar.localScale = new Vector3((float)_health.currentHealth / (float)_health.maxHealth, 1, 1);
            healthText.text =  _health.currentHealth.ToString() + " / " + _health.maxHealth.ToString();
        }
    }
}
