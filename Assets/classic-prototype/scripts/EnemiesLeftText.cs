using System;
using TMPro;
using UnityEngine;

namespace Classic
{
    public class EnemiesLeftText : MonoBehaviour
    {
        public EnemySpawner enemySpawner;
        private TextMeshProUGUI _text;

        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            _text.text = "Enemies Left: " + (enemySpawner.enemiesToSpawn + enemySpawner.currentEnemies);
        }
    }
}