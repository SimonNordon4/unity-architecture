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

        [Header("Player")] public GameObject player;
        public float PlayerMoveSpeed = 5.0f;
        public int playerMaxHealth;
        public int playerCurrentHealth;

        #endregion

        #region Gun Variables

        [Header("Gun")] public Bullet bulletPrefab;
        public float bulletCooldown;
        private float _timeSinceLastBullet = float.PositiveInfinity;
        public float bulletSpeed = 10.0f;
        public float bulletLifeTime = 2.0f;
        public int bulletDamage = 1;
        private List<BulletObject> _bullets = new List<BulletObject>();

        #endregion

        #region EnemySpawnerVariables

        [Header("enemy Spawner")] public Enemy enemyPrefab;

        // Probabilities of each enemy type spawning
        public float smallEnemySpawnChance = 0.1f;
        public float bigEnemySpawnChance = 0.03f;

        // maximum enemies to spawn in level
        public int enemiesToSpawn = 100;

        [ReadOnly] public int currentEnemies = 0;
        public int maxCurrentEnemies = 50;

        public float enemySpawnRate = 1f;

        // every time an enemy spawns, the next enemy will spawn slightly faster.
        public float maxEnemySpawnRate = 0.1f;
        private float _enemySpawnRateIncrement = 0.0f;
        private float _timeSinceLastEnemySpawn = 0f;

        public Vector2 minMaxSpawnDistance = new Vector2(5f, 10f);

        private List<EnemyObject> _enemies = new List<EnemyObject>();

        #endregion

        #region EnemyVariables

        [Header("Enemies")] public EnemyData enemy;
        public EnemyData smallEnemy;
        public EnemyData bigEnemy;

        #endregion

        #endregion


        #region Local Classes

        internal class BulletObject
        {
            public Bullet Bullet;
            public Vector3 direction;
            public float timeAlive = 0.0f;
        }

        public class EnemyObject
        {
            public Enemy enemy;
            public int currentHealth;
            public EnemyData data;
        }

        #endregion


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
            CheckWinCondition();
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
            Debug.Log("You Win!");
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
                        var bulletObject = new BulletObject();
                        bulletObject.Bullet = Instantiate(bulletPrefab, player.transform.position, Quaternion.identity);
                        bulletObject.direction = bulletDirection.normalized;
                        _timeSinceLastBullet = 0.0f;
                        _bullets.Add(bulletObject);
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
                    var spawnSmallEnemy = Random.Range(0, 100) < smallEnemySpawnChance * 100f;
                    if (spawnSmallEnemy)
                    {
                        enemyData = smallEnemy;
                    }

                    // roll to see if we spawn a big enemy.
                    var spawnBigEnemy = Random.Range(0, 100) < bigEnemySpawnChance * 100f;
                    if (spawnBigEnemy)
                        enemyData = bigEnemy;

                    // create and initialise the enemy.

                    var newEnemyObject = new EnemyObject
                    {
                        enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity),
                        data = enemyData,
                        currentHealth = enemyData.maxHealth
                    };
                    newEnemyObject.enemy.Parent = newEnemyObject;

                    _enemies.Add(newEnemyObject);

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
            foreach (var enemyObject in _enemies)
            {
                var enemyTransform = enemyObject.enemy.transform;
                var directionToPlayer = (player.transform.position - enemyTransform.position).normalized;
                Move(enemyTransform, directionToPlayer, enemyObject.data.moveSpeed);
            }
        }

        private void BulletUpdate()
        {
            var speed = this.bulletSpeed * Time.deltaTime;

            for (int i = 0; i < _bullets.Count; i++)
            {
                // check the lifetime
                var bulletObject = _bullets[i];
                if (bulletObject.timeAlive > bulletLifeTime)
                {
                    _bullets.Remove(bulletObject);
                    Destroy(bulletObject.Bullet.gameObject);
                }
                else
                {
                    bulletObject.Bullet.transform.Translate(bulletObject.direction * speed);
                    bulletObject.timeAlive += Time.deltaTime;
                }
            }
        }


        #region reusable methods

        private void Move(Transform target, Vector3 direction, float moveSpeed)
        {
            target.Translate(direction.normalized * (moveSpeed * Time.deltaTime));
        }

        public void BulletHit(Bullet bullet, Collider other)
        {
            // check if the bullet hit an enemy
            if (other.CompareTag("Enemy"))
            {
                var enemyObject = other.GetComponent<Enemy>().Parent;

                // Remove bullet damage from the enemies health.
                // If it take lethal damage, destroy the enemy.
                enemyObject.currentHealth -= bulletDamage;
                if (enemyObject.currentHealth <= 0)
                {
                    currentEnemies--;
                    _enemies.Remove(enemyObject);
                    Destroy(enemyObject.enemy.gameObject);
                }
            }
        }

        public void EnemyHit(Enemy enemy, Collider other)
        {
            // If the enemy hit a player, reduce the players health.
            if (other.CompareTag("Player"))
            {
                playerCurrentHealth -= enemy.Parent.data.damage;
                
                // If the player has taken lethal damage, end the game.
                if (playerCurrentHealth <= 0)
                {
                    LoseGame();
                }
            }
        }

        public void LoseGame()
        {
            Debug.Log("You Lose!");
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
        public float size;
        public Material material;
    }
}