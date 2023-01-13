using System;
using TMPro;
using UnityEngine;

namespace Architecture.Classic
{
    /// <summary>
    /// Polls a gun component and displays its current cooldown.
    /// </summary>
    public class BulletCooldownText : MonoBehaviour
    {
        public Gun gun;
        private TextMeshProUGUI _text;

        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            // gun.bulletCooldown in seconds rounder to the nearest two decimal places
            _text.text = "Bullet Cooldown: " + gun.bulletCooldown.ToString("F2") + "s";
        }
    }
}