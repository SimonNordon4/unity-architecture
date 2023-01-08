using System;
using System.Collections.Generic;
using Classic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace GodObject
{
    public class God : UnityEngine.MonoBehaviour
    {
        #region Declerations

        private static God _instance;

        public static God Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<God>();
                }

                return _instance;
            }
        }

        [ReadOnly] public GameState gameState = GameState.Menu;
        public Transform camera;
        public Vector3 cameraOffset;

        #region Player Variables
        
        [Header("Player")]
        public GameObject player;
        public float PlayerMoveSpeed = 5.0f;
        public int playerMaxHealth;
        public int playerCurrentHealth;
        
        #endregion

        #region Gun Variables
        [Header("Gun")]
        public Bullet bulletPrefab;
        public float bulletCooldown;
        private float _timeSinceLastBullet = float.PositiveInfinity;
        public float bulletSpeed = 10.0f;
        public float bulletLifeTime = 2.0f;
        public int bulletDamage = 1;
        private List<Bullet> _bullets = new List<Bullet>();

        #endregion
        
        #region EnemySpawnerVariables
        [Header("Enemy Spawner")]
        public Enemy enemyPrefab;
        // Probabilities of each enemy type spawning
        public float smallEnemySpawnChance = 0.1f;
        public float bigEnemySpawnChance = 0.03f;

        // maximum enemies to spawn in level
        public int enemiesToSpawn = 100;

        [ReadOnly]
        public int currentEnemies = 0;
        public int maxCurrentEnemies = 50;

        public float enemySpawnRate = 1f;
        // every time an enemy spawns, the next enemy will spawn slightly faster.
        public float maxEnemySpawnRate = 0.1f;
        private float _enemySpawnRateIncrement = 0.0f;
        private float _timeSinceLastEnemySpawn = 0f;

        public Vector2 minMaxSpawnDistance = new Vector2(5f, 10f);
        
        private List<Enemy> _enemies = new List<Enemy>();
        #endregion
        
        #region EnemyVariables

        [Header("Enemies")] 
        public EnemyData enemy;
        public EnemyData smallEnemy;
        public EnemyData bigEnemy;
        #endregion
        
        #endregion

        internal class BulletObject
        {
            public Bullet Bullet;
        }
        
        internal class EnemyObject
        {
            public Enemy Enemy;
        }

        void Start()
        {
            // initialise player
            playerCurrentHealth = playerMaxHealth;
            
            // initialise camera
            cameraOffset = camera.transform.position;
        }

        private void Update()
        {
            PlayerUpdate();
            EnemySpawnerUpdate();
            EnemyUpdate();
            BulletUpdate();
        }


        
        private void PlayerUpdate()
        {
            // Move the player.
            var moveDirection = Vector3.zero;
            if (Input.GetKey(KeyCode.D))
            {
                moveDirection.x += 1;
            }

            if (Input.GetKey(KeyCode.A))
            {
                moveDirection.x -= 1;
            }

            if (Input.GetKey(KeyCode.W))
            {
                moveDirection.z += 1;
            }

            if (Input.GetKey(KeyCode.S))
            {
                moveDirection.z -= 1;
            }

            Move(player.transform, moveDirection, PlayerMoveSpeed);
            
            // lock the camera to the player
            camera.transform.position = player.transform.position + cameraOffset;

            _timeSinceLastBullet += Time.deltaTime;
            // Fire a bullet.
            if (Input.GetMouseButton(0))
            {
                
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit))
                {
                    var bulletDirection = hit.point - player.transform.position;
                    bulletDirection.y = 0;

                    if (_timeSinceLastBullet > bulletCooldown)
                    {
                        Debug.Log("Bulleto");
                        var bullet = Instantiate(bulletPrefab, player.transform.position, Quaternion.identity);
                        bullet.direction = bulletDirection.normalized;
                        _timeSinceLastBullet = 0.0f;
                        _bullets.Add(bullet);
                    }
                }
            }
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

                    var enemyData = enemy;
                
                    // roll to see if we spawn a small enemy.
                    var spawnSmallEnemy = Random.Range(0,100) < smallEnemySpawnChance * 100f;
                    if (spawnSmallEnemy)
                    {
                        enemyData = smallEnemy;
                    }

                    // roll to see if we spawn a big enemy.
                    var spawnBigEnemy = Random.Range(0,100) < bigEnemySpawnChance *100f;
                    if (spawnBigEnemy)
                        enemyData = bigEnemy;
                
                    // create and initialise the enemy.
                    var newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                    newEnemy.data = enemyData;
                    newEnemy.currentHealth = enemyData.maxHealth;
                    _enemies.Add(newEnemy);
                    
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
            foreach (var enemy1 in _enemies)  
            {
                var enemyTransform = enemy1.transform;
                var directionToPlayer = (player.transform.position - enemyTransform.position).normalized;
                Move(enemyTransform, directionToPlayer, enemy1.data.moveSpeed);
            }
        }
        
        private void BulletUpdate()
        {
            var speed = this.bulletSpeed * Time.deltaTime;

            for(int i=0; i < _bullets.Count; i++)
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
        

        
        #region reusable methods
        private void Move(Transform target, Vector3 direction, float moveSpeed)
            {
                target.Translate(direction.normalized * (moveSpeed * Time.deltaTime));
            }
        
        #endregion

            #region Scene Changes

            public void LoadMenu() => SceneManager.LoadScene("GodObject_Menu");

            public void LoadLevel1() => SceneManager.LoadScene("GodObject_Game_Level01");

            public void LoadLevel2() => SceneManager.LoadScene("GodObject_Game_Level02");

            public void LoadLevel3() => SceneManager.LoadScene("GodObject_Game_Level03");

            public void Quit()
            {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }

            #endregion
        }

        public enum GameState
        {
            Menu,
            Game,
            Won,
            Lost
        }
        
        [Serializable]
        public struct EnemyData
        {
            public int maxHealth;
            public float moveSpeed;
            public int damage;
        }
    }




