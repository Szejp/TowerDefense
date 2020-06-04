using System;
using System.Collections.Generic;
using AFSInterview.GameElements;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AFSInterview.System.Spawning
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private SpawnerConfig config;

        private readonly List<Enemy> spawnedEnemies = new List<Enemy>();
        
        private float enemySpawnTimer;
        private Vector2 boundsMin;
        private Vector2 boundsMax;
        private float enemySpawnRate;

        public List<Enemy> SpawnedEnemies => spawnedEnemies;

        public static event Action<int> OnEnemiesCountChanged;

        public void SetBounds(Vector2 boundsMin, Vector2 boundsMax)
        {
            this.boundsMin = boundsMin;
            this.boundsMax = boundsMax;
        }

        public void SetEnemySpawnRate(float enemySpawnRate)
        {
            this.enemySpawnRate = enemySpawnRate;
        }
        
        public void SpawnTower(Vector3 position)
        {
            var tower = Instantiate(config.towerPrefab, new Vector3(position.x, config.towerPrefab.layer, position.z),
                Quaternion.identity).GetComponent<SimpleTower>();
            tower.Initialize(spawnedEnemies);
        }

        public void TrySpawnEnemy()
        {
            if (enemySpawnTimer > 0f)
                return;

            var position = new Vector3(Random.Range(boundsMin.x, boundsMax.x), config.enemyPrefab.transform.position.y,
                Random.Range(boundsMin.y, boundsMax.y));
            var enemy = Instantiate(config.enemyPrefab, position, Quaternion.identity).GetComponent<Enemy>();
            enemy.Initialize(boundsMin, boundsMax);
            spawnedEnemies.Add(enemy);
            OnEnemiesCountChanged?.Invoke(SpawnedEnemies.Count);
            enemySpawnTimer = enemySpawnRate;
        }

        private void Awake()
        {
            Enemy.OnEnemyDied += OnEnemyDiedHandler;
        }

        private void OnDestroy()
        {
            Enemy.OnEnemyDied -= OnEnemyDiedHandler;
        }

        private void Update()
        {
            enemySpawnTimer -= Time.deltaTime;
            TrySpawnEnemy();
        }

        private void OnEnemyDiedHandler(Enemy enemy)
        {
            SpawnedEnemies.Remove(enemy);
            OnEnemiesCountChanged?.Invoke(SpawnedEnemies.Count);
        }
    }
}