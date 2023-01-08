using System;
using System.Collections.Generic;
using Classic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        #region Player Variables
        [Header("Player")]
        public GameObject Player;
        public float PlayerMoveSpeed = 5.0f;
        public int maxHealth;
        public int currentHealth;
        #endregion

        #region Gun Variables
        [Header("Gun")]
        public Bullet bulletPrefab;
        public float bulletCooldown;
        private float _timeSinceLastBullet = float.PositiveInfinity;
        public float bulletSpeed = 10.0f;
        public float bulletLifeTime = 2.0f;
        public int bulletDamage = 1;
        public List<Bullet> bullets = new List<Bullet>();

        #endregion
        
        #region EnemySpawnerVariables
        [Header("Enemy Spawner")]
        public Enemy enemyPrefab;
        // Probabilities of each enemy type spawning
        public float smallEnemySpawnChance = 0.1f;
        public float bigEnemySpawnChance = 0.03f;

        // maximum enemies to spawn in level
        public int enemiesToSpawn = 100;

        public int currentEnemies = 0;
        public int maxCurrentEnemies = 50;

        public float enemySpawnRate = 1f;
        // every time an enemy spawns, the next enemy will spawn slightly faster.
        public float maxEnemySpawnRate = 0.1f;
        private float _enemySpawnRateIncrement = 0.0f;
        private float _timeSinceLastEnemySpawn = 0f;

        public Transform playerTransform;
        public Vector2 minMaxSpawnDistance = new Vector2(5f, 10f);
        #endregion
        
        #region EnemyVariables

        [Header("Enemies")] 
        public EnemyData enemy;
        public EnemyData smallEnemy;
        public EnemyData bigEnemy;
        #endregion
        
        #endregion

        void Start()
        {

        }

        private void Update()
        {
            PlayerUpdate();
            BulletUpdate();
        }


        
        private void PlayerUpdate()
        {
            // Move the Player.
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

            Move(Player.transform, moveDirection, PlayerMoveSpeed);

            // Fire a bullet.
            if (Input.GetMouseButton(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit))
                {
                    var bulletDirection = hit.point - transform.position;
                    bulletDirection.y = 0;

                    if (_timeSinceLastBullet > bulletCooldown)
                    {
                        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                        bullet.direction = bulletDirection;
                        _timeSinceLastBullet = 0.0f;
                        bullets.Add(bullet);
                    }
                }
            }
        }
        
        private void EnemySpawnerUpdate()
        {
            
        }

        private void EnemyUpdate()
        {
            
        }
        
        private void BulletUpdate()
        {
            foreach (var bullet in bullets)
            {
                // check the lifetime
                if (bullet.timeAlive > bulletLifeTime)
                {
                    bullets.Remove(bullet);
                    Destroy(bullet.gameObject);
                }
                else
                {
                    Move(bullet.transform, bullet.direction, bulletSpeed);
                    bullet.timeAlive += Time.deltaTime;
                }
            }
        }
        

        
        #region reusable methods
        private void Move(Transform target, Vector3 direction, float moveSpeed)
            {
                transform.Translate(direction * (moveSpeed * Time.deltaTime));
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
            public int health;
            public float speed;
            public int damage;
        }
    }




