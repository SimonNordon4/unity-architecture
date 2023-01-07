using System;
using UnityEngine;

namespace Classic
{
    public class GameManager : MonoBehaviour
    {
        public Health playerHealth;
        public EnemySpawner enemySpawner;

        public void Update()
        {
            if (enemySpawner.enemiesToSpawn <= 0 && enemySpawner.currentEnemies <= 0)
            {
                Win();
            }
            
            if (playerHealth.currentHealth <= 0)
            {
                Lose();
            }
        }
        
        public void Win()
        {
            Debug.Log("You Win!");
        }
        
        public void Lose()
        {
            var enemies = FindObjectsOfType<Enemy>();
            var bullets = FindObjectsOfType<Bullet>();

            for(int i = 0; i < enemies.Length; i++)
            {
                Destroy(enemies[i].gameObject);
            }
            
            for(int i = 0; i < bullets.Length; i++)
            {
                Destroy(bullets[i].gameObject);
            }
        }


    }
}