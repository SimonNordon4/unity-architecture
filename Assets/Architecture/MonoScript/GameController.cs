using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Architecture.MonoScript
{
    /// <summary>
    /// Controls the game logic for this scene.
    /// Representative of how a new developer might approach game development.
    /// This class is responsible for:
    ///     Controlling the Camera
    ///     Controlling the Player Input
    ///     Spawning Enemies
    ///     Updating Enemies
    ///     Spawning Bullets
    ///     Updating Bullets
    ///     Resolving Collisions (Attacks)
    ///     Checking if the game has been won.
    ///     Controlling UI State.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        // Tracks whether or not we're in play mode.
        public bool isGamePlaying = true;
        
        [Header("Camera")]
        
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
        
        // These fields allow us to accelerate the spawn rate of enemies, increasing the difficulty.
        public float minEnemySpawnRate = 0.1f;
        private float _enemySpawnRateIncrement;
        private float _timeSinceLastEnemySpawn = 0f;

        // How far away the enemies can spawn from the player.
        public Vector2 minMaxSpawnDistance = new Vector2(5f, 10f);

        private List<Enemy> _enemies = new List<Enemy>();

        [Header("User Interface")] 
        public TextMeshProUGUI HealthText;
        public TextMeshProUGUI CurrentEnemiesText;
        public GameObject GameOverScreen;
        public TextMeshProUGUI GameOverText;
        

        void Start()
        {
            // Initialise player.
            playerCurrentHealth = playerMaxHealth;

            // Initialise camera.
            cameraOffset = camera.transform.position;
            
            // Calculate our enemy spawn rate increment.
            _enemySpawnRateIncrement = (enemySpawnRate - minEnemySpawnRate) / enemiesToSpawn;
        }

        private void Update()
        {
            // We stop updating all our systems when the game is over. We rely on button events to restart the game.
            if (isGamePlaying)
            {
                PlayerUpdate();
                CameraUpdate();
                EnemySpawnerUpdate();
                EnemyUpdate();
                GunUpdate();
                BulletUpdate();
                CheckWinCondition();
                UpdateUI();
            }
        }
        private void PlayerUpdate()
        {
            // Read keyboard input and map it to a direction
            var moveDirection = Vector3.zero;
            if (Input.GetKey(KeyCode.D))
                moveDirection.x += 1;

            if (Input.GetKey(KeyCode.A))
                moveDirection.x -= 1;

            if (Input.GetKey(KeyCode.W))
                moveDirection.z += 1;

            if (Input.GetKey(KeyCode.S))
                moveDirection.z -= 1;

            // Ensure all directions are normalized!
            player.transform.Translate(moveDirection.normalized * (PlayerMoveSpeed * Time.deltaTime));
        }

        private void CameraUpdate()
        {
            // lock the camera to the player
            camera.transform.position = player.transform.position + cameraOffset;
        }
        
        private void EnemySpawnerUpdate()
        {
            // This is our enemy spawn cooldown.
            _timeSinceLastEnemySpawn += Time.deltaTime;
            
            // Check if it's time to spawn an enemy.
            if (_timeSinceLastEnemySpawn >= enemySpawnRate && enemiesToSpawn > 0)
            {
                if (currentEnemies < maxCurrentEnemies)
                {
                    // Generate a random position at a chosen radius from the player.
                    Vector2 randomDirection = Random.insideUnitCircle.normalized;
                    Vector3 randomWorldDirection = new Vector3(randomDirection.x, 0f, randomDirection.y);
                    var randomDistance = Random.Range(minMaxSpawnDistance.x, minMaxSpawnDistance.y);
                    Vector3 spawnPosition = randomDistance * randomWorldDirection + player.transform.position;

                    // Create and initialise the enemy.
                    Enemy enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                    enemy.gameController = this;
                    enemy.currentHealth = enemy.maxHealth;
                    _enemies.Add(enemy);
                    
                    // Update the enemy spawner data.
                    currentEnemies++;
                    enemiesToSpawn--;

                    // Increase the spawn rate of the next enemy.
                    enemySpawnRate -= _enemySpawnRateIncrement;
                }

                // Reset the spawn cooldown.
                _timeSinceLastEnemySpawn = 0f;
            }
        }
        
        private void EnemyUpdate()
        {
            // Move each enemy closer to the player.
            foreach (var enemy in _enemies)
            {
                var enemyTransform = enemy.transform;
                var directionToPlayer = (player.transform.position - enemyTransform.position).normalized;
                enemyTransform.Translate( directionToPlayer * (enemy.moveSpeed * Time.deltaTime));
            }
        }

        /// <summary>
        /// Controls the firing of bullets.
        /// </summary>
        private void GunUpdate()
        {
            // This is our gun firing cooldown.
            _timeSinceLastBullet += Time.deltaTime;
            
            // Check if it's time to spawn a bullet.
            if (_timeSinceLastBullet > bulletCooldown)
            {
                
                // In order to fire a bullet, we first need to find the closest enemy.
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
                
                // Ensure there is an enemy on the map.
                if (closestEnemy != null)
                {
                    var enemyDistance = Vector3.Distance(player.transform.position, 
                        closestEnemy.transform.position);
                    
                    // If there is an enemy, ensure that it's within the guns range.
                    if (enemyDistance < gunRange)
                    {
                        // Create and Initialize a bullet, and reset the gun cooldown.
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
                // Check the bullet lifetime. This prevents the bullets living forever.
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
        
        /// <summary>
        /// This will be called when a GameObject with a Bullet component enters a trigger.
        /// </summary>
        public void BulletHit(Bullet bullet, Collider other)
        {
            // Check if the bullet hit an enemy,.
            if (other.CompareTag("Enemy"))
            {
                var enemy = other.GetComponent<Enemy>();

                // Remove bullet damage from the enemies health.
                enemy.currentHealth -= bulletDamage;
                // If the enemy has run out of health, kill it and update the Enemy Spawner.
                if (enemy.currentHealth <= 0)
                {
                    currentEnemies--;
                    _enemies.Remove(enemy);
                    Destroy(enemy.gameObject);
                }
                
                // Destroy the bullet.
                _bullets.Remove(bullet);
                Destroy(bullet.gameObject);
            }
        }
        
        /// <summary>
        /// This will be called when a GameObject with an Enemy Component on it enters a trigger.
        /// </summary>
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
            // If the enemy spawner has run out of enemies, and all enemies on the field are dead, the game is won.
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

        /// <summary>
        /// Reset the game by reloading the scene. 
        /// </summary>
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