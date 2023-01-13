using System;
using TMPro;
using UnityEngine;

namespace Architecture.Classic.Plus
{
    /// <summary>
    /// Polls a gun component and displays its current cooldown.
    /// </summary>
    public class BulletCooldownText : MonoBehaviour
    {
        private Gun _gun;
        private TextMeshProUGUI _text;

        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _gun = GameCatalog.Instance.Player.GetComponent<Gun>();
        }

        private void Update()
        {
            // gun.bulletCooldown in seconds rounder to the nearest two decimal places
            _text.text = "Bullet Cooldown: " + _gun.bulletCooldown.ToString("F2") + "s";
        }
    }
}