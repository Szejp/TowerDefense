using System.Collections.Generic;
using AFSInterview.Shooting;
using UnityEngine;

namespace AFSInterview.GameElements
{
    public class SimpleTower : MonoBehaviour
    {
        [SerializeField] private ShootingSystem shootingSystem;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private float firingRange;

        private float fireTimer;
        private Enemy targetEnemy;

        private IReadOnlyList<Enemy> enemies;

        public ShootingSystem ShootingSystem => shootingSystem;

        public void Initialize(IReadOnlyList<Enemy> enemies)
        {
            this.enemies = enemies;
            fireTimer = ShootingSystem.FireRate;
        }

        private void Update()
        {
            targetEnemy = FindClosestEnemy();
            if (targetEnemy != null)
            {
                var lookRotation = Quaternion.LookRotation(targetEnemy.transform.position - transform.position);
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lookRotation.eulerAngles.y,
                    transform.rotation.eulerAngles.z);
            }

            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0f)
            {
                if (targetEnemy != null)
                    ShootingSystem.Fire(targetEnemy.gameObject.transform);

                fireTimer = ShootingSystem.FireRate;
            }
        }

        private Enemy FindClosestEnemy()
        {
            Enemy closestEnemy = null;
            var closestDistance = float.MaxValue;

            foreach (var enemy in enemies)
            {
                var distance = (enemy.transform.position - transform.position).magnitude;
                if (distance <= firingRange && distance <= closestDistance)
                {
                    closestEnemy = enemy;
                    closestDistance = distance;
                }
            }

            return closestEnemy;
        }
    }
}