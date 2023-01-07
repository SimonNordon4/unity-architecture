using System;
using UnityEngine;

namespace Classic
{
    public class GameManager : MonoBehaviour
    {
        public Health playerHealth;
        public EnemySpawner enemySpawner;

        public GameObject winScreen;
        public GameObject loseScreen;

        public void Update()
        {
            if (enemySpawner.enemiesToSpawn <= 0 && enemySpawner.currentEnemies <= 0)
            {
                EndGame();
                winScreen.SetActive(true);
            }
            
            if (playerHealth.currentHealth <= 0)
            {
                EndGame();
                loseScreen.SetActive(true);
            }
        }
        
        private void EndGame()
        {
            // disable the camera follower.
            Camera.main.GetComponent<FollowTransform>().enabled = false;
            
            // disable the spawners
            enemySpawner.enabled = false;
            FindObjectOfType<HealthPackSpawner>().enabled = false;
            FindObjectOfType<BulletBoostSpawner>().enabled = false;
            
            // disable the player
            var player = FindObjectsOfType<Player>();
            var enemies = FindObjectsOfType<Enemy>();
            var bullets = FindObjectsOfType<Bullet>();
            
            for(int i = 0; i < player.Length; i++)
            {
                Destroy(player[i].gameObject);
            }

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