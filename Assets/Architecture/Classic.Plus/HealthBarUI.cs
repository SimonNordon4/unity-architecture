using System;
using TMPro;
using UnityEngine;

namespace Architecture.Classic.Plus
{
    /// <summary>
    /// A UI element that displays a parents Health Component Data.
    /// </summary>
    public class HealthBarUI : MonoBehaviour
    {
        private Health _health;
        [SerializeField]
        private RectTransform healthBar;
        [SerializeField]
        private TextMeshProUGUI healthText;
       
        private void Start()
        {
            _health = GetComponentInParent<Health>();
        }

        private void Update()
        {
            healthBar.localScale = new Vector3((float)_health.Current / (float)_health.Max, 1, 1);
            healthText.text =  _health.Current.ToString() + " / " + _health.Max.ToString();
        }
    }
}

