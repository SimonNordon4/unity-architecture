using System;
using UnityEngine;

namespace Architecture.Classic
{
    /// <summary>
    /// Monitors and controls the state of the gameplay level.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public Health playerHealth;
        public EnemySpawner enemySpawner;

        public GameObject winScreen;
        public GameObject loseScreen;

        public void Update()
        {
            // Check for a win or lose condition on every frame.
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
        
        /// <summary>
        /// Ends the game by destroying or disabling ever game-object in the scene.
        /// </summary>
        private void EndGame()
        {
            // disable the camera follower.
            Camera.main.GetComponent<FollowPosition>().enabled = false;
            
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