using System;
using System.Collections.Generic;
using AFSInterview.GameElements;
using AFSInterview.Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AFSInterview.System.Spawning
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private SpawnerConfig config;

        private readonly List<Enemy> spawnedEnemies = new List<Enemy>();
        private readonly ObjectPool pool = new ObjectPool();
        
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
        
        public void SpawnTower(Vector3 position, TowerType towerType)
        {
            var towerToSpawn = towerType == TowerType.Simple ? config.towerPrefab : config.otherTowerPrefab;
            var tower = SpawnObject(towerToSpawn, new Vector3(position.x, config.towerPrefab.transform.position.y, position.z)) as SimpleTower;
            tower.Initialize(spawnedEnemies);
            tower.ShootingSystem?.SetSpawner(this);
        }

        public Component Spawn(Component objToSpawn, Vector3 position)
        {
            return SpawnObject(objToSpawn, position);
        }

        public void TrySpawnEnemy()
        {
            if (enemySpawnTimer > 0f)
                return;

            var position = new Vector3(Random.Range(boundsMin.x, boundsMax.x), config.enemyPrefab.transform.position.y,
                Random.Range(boundsMin.y, boundsMax.y));
            var enemy = SpawnObject(config.enemyPrefab, position) as Enemy;
            enemy.Initialize(boundsMin, boundsMax);
            spawnedEnemies.Add(enemy);
            OnEnemiesCountChanged?.Invoke(SpawnedEnemies.Count);
            enemySpawnTimer = enemySpawnRate;
        }

        private Component SpawnObject(Component objToSpawn, Vector3 position)
        {
            var obj = pool.GetFreeObject(objToSpawn);
            obj.transform.position = position;
            return obj;
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