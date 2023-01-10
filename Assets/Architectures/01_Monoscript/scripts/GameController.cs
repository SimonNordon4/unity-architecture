using System;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace MonoScript
{
    /// <summary>
    /// Controls all of the Scene Logic.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        public bool isGamePlaying = true;
        
        public Transform camera;
        public Vector3 cameraOffset;

        [Header("Player")] 
        
        public GameObject player;
        public float PlayerMoveSpeed = 5.0f;
        public int playerMaxHealth;
        public int playerCurrentHealth;

        [Header("Gun")] 
        
        public float gunRange = 5f;
        public Bullet bulletPrefab;
        public float bulletCooldown;
        private float _timeSinceLastBullet = float.PositiveInfinity;
        public float bulletSpeed = 10.0f;
        public float bulletLifeTime = 2.0f;
        public int bulletDamage = 1;
        
        private List<Bullet> _bullets = new List<Bullet>();

        [Header("Enemy Spawner")] 
        
        public Enemy enemyPrefab;
        // maximum enemies to spawn in level
        public int enemiesToSpawn = 100;
        public int currentEnemies = 0;
        public int maxCurrentEnemies = 50;

        public float enemySpawnRate = 1f;

        // every time an enemy spawns, the next enemy will spawn slightly faster.
        public float maxEnemySpawnRate = 0.1f;
        private float _enemySpawnRateIncrement = 0.0f;
        private float _timeSinceLastEnemySpawn = 0f;

        public Vector2 minMaxSpawnDistance = new Vector2(5f, 10f);

        private List<Enemy> _enemies = new List<Enemy>();

        [Header("User Interface")] 
        public TextMeshProUGUI HealthText;
        public TextMeshProUGUI CurrentEnemiesText;
        public GameObject GameOverScreen;
        public TextMeshProUGUI GameOverText;

        void Start()
        {
            // initialise player
            playerCurrentHealth = playerMaxHealth;

            // initialise camera
            cameraOffset = camera.transform.position;
        }

        private void Update()
        {
            if (isGamePlaying)
            {
                PlayerUpdate();
                EnemySpawnerUpdate();
                EnemyUpdate();
                BulletUpdate();
                CheckWinCondition();
                UpdateUI();
            }
        }
        private void PlayerUpdate()
        {
            // Move the player.
            var moveDirection = Vector3.zero;
            if (Input.GetKey(KeyCode.D))
                moveDirection.x += 1;

            if (Input.GetKey(KeyCode.A))
                moveDirection.x -= 1;

            if (Input.GetKey(KeyCode.W))
                moveDirection.z += 1;

            if (Input.GetKey(KeyCode.S))
                moveDirection.z -= 1;

            player.transform.Translate(moveDirection * (PlayerMoveSpeed * Time.deltaTime));

            // lock the camera to the player
            camera.transform.position = player.transform.position + cameraOffset;

            _timeSinceLastBullet += Time.deltaTime;
        }
        private void EnemySpawnerUpdate()
        {
            _timeSinceLastEnemySpawn += Time.deltaTime;
            if (_timeSinceLastEnemySpawn >= enemySpawnRate && enemiesToSpawn > 0)
            {
                if (currentEnemies < maxCurrentEnemies)
                {
                    // create a random spawn position within the spawn area that is 1 away from the player position
                    Vector2 randomDirection = Random.insideUnitCircle.normalized;
                    Vector3 randomWorldDirection = new Vector3(randomDirection.x, 0f, randomDirection.y);
                    var randomDistance = Random.Range(minMaxSpawnDistance.x, minMaxSpawnDistance.y);
                    Vector3 spawnPosition = randomDistance * randomWorldDirection + player.transform.position;

                    // create and initialise the enemy.
                    Enemy enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                    enemy.gameController = this;
                    enemy.currentHealth = enemy.maxHealth;
                    _enemies.Add(enemy);
                    // update enemy data.
                    currentEnemies++;
                    enemiesToSpawn--;

                    // increase the spawn rate of the next enemy.
                    enemySpawnRate -= _enemySpawnRateIncrement;
                }

                _timeSinceLastEnemySpawn = 0f;
            }
        }
        private void EnemyUpdate()
        {
            foreach (var enemy in _enemies)
            {
                var enemyTransform = enemy.transform;
                var directionToPlayer = (player.transform.position - enemyTransform.position).normalized;
                enemyTransform.Translate( directionToPlayer * (enemy.moveSpeed * Time.deltaTime));
            }
        }

        private void GunUpdate()
        {
            // get closest enemy
            Enemy closestEnemy = null;
            float closestEnemyDistance = float.PositiveInfinity;
            foreach (var enemy in _enemies)
            {
                var distanceToEnemy = Vector3.Distance(player.transform.position, enemy.transform.position);
                if (distanceToEnemy < closestEnemyDistance)
                {
                    closestEnemy = enemy;
                    closestEnemyDistance = distanceToEnemy;
                }
            }
            
            // Check if we've come off cooldown.
            if (_timeSinceLastBullet > bulletCooldown)
            {
                if (closestEnemy != null)
                {
                    var enemyDistance = Vector3.Distance(player.transform.position, 
                        closestEnemy.transform.position);
                    
                    if (enemyDistance < gunRange)
                    {
                        var playerPosition = player.transform.position;
                        var bullet = Instantiate(bulletPrefab, playerPosition, Quaternion.identity);
                        bullet.direction = (closestEnemy.transform.position - playerPosition).normalized;
                        bullet.gameController = this;
                        _timeSinceLastBullet = 0.0f;
                        _bullets.Add(bullet);
                    }
                }
            }
        }
        private void BulletUpdate()
        {
            var speed = bulletSpeed * Time.deltaTime;
            for (int i = 0; i < _bullets.Count; i++)
            {
                // check the lifetime
                var bullet = _bullets[i];
                if (bullet.timeAlive > bulletLifeTime)
                {
                    _bullets.Remove(bullet);
                    Destroy(bullet.gameObject);
                }
                else
                {
                    bullet.transform.Translate(bullet.direction * speed);
                    bullet.timeAlive += Time.deltaTime;
                }
            }
        }
        private void UpdateUI()
        {
            HealthText.text = $"Health: {playerCurrentHealth}/{playerMaxHealth}";
            CurrentEnemiesText.text = $"Enemies Left {enemiesToSpawn + currentEnemies}";
        }
        public void BulletHit(Bullet bullet, Collider other)
        {
            // check if the bullet hit an enemy
            if (other.CompareTag("Enemy"))
            {
                var enemy = other.GetComponent<Enemy>();

                // Remove bullet damage from the enemies health.
                // If it take lethal damage, destroy the enemy.
                enemy.currentHealth -= bulletDamage;
                if (enemy.currentHealth <= 0)
                {
                    currentEnemies--;
                    _enemies.Remove(enemy);
                    Destroy(enemy.gameObject);
                }
                
                // destroy the bullet
                _bullets.Remove(bullet);
                Destroy(bullet.gameObject);
            }
        }
        public void EnemyHit(Enemy enemy, Collider other)
        {
            // If the enemy hit a player, reduce the players health.
            if (other.CompareTag("Player"))
            {
                playerCurrentHealth -= enemy.damage;
                
                // If the player has taken lethal damage, end the game.
                if (playerCurrentHealth <= 0)
                {
                    LoseGame();
                }
            }
        }
        private void CheckWinCondition()
        {
            if(enemiesToSpawn <= 0 && currentEnemies <= 0)
            {
                WinGame();
            }
        }

        private void WinGame()
        {
            GameOverScreen.SetActive(true);
            GameOverText.text = "You Win!";
            isGamePlaying = false;
        }

        private void LoseGame()
        {
            GameOverScreen.SetActive(true);
            GameOverText.text = "You Lose!";
            isGamePlaying = false;
        }

        public void StartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}