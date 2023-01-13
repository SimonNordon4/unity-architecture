using System;
using TMPro;
using UnityEngine;

namespace Architecture.Classic.Plus
{
    /// <summary>
    /// Polls an enemy spawner component and displays the number of enemies remaining.
    /// </summary>
    public class EnemiesLeftText : MonoBehaviour
    {
        private EnemySpawner _enemySpawner;
        private TextMeshProUGUI _text;

        private void Start()
        {
            _enemySpawner = GameCatalog.Instance.EnemySpawner;
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            _text.text = "Enemies Left: " + (_enemySpawner.enemiesToSpawn + _enemySpawner.currentEnemies);
        }
    }
}