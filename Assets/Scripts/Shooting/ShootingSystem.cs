using System.Collections;
using AFSInterview.GameElements;
using AFSInterview.System.Spawning;
using UnityEngine;

namespace AFSInterview.Shooting
{
    public class ShootingSystem : MonoBehaviour
    {
        [SerializeField] ShootingSystemConfig config;
        [SerializeField] Transform shootingAnchor;
        [SerializeField] Spawner spawner;

        Vector3 directionVector;
        Vector3 lastTargetPosition;
        float[] angleSpreads;
        Vector3 posToShootAt;

        public float FireRate => config.fireRate;

        public void SetSpawner(Spawner spawner)
        {
            this.spawner = spawner;
        }

        [ContextMenu("Fire")]
        public void Fire(Transform target)
        {
            StartCoroutine(FireCoroutine(target));
        }

        IEnumerator FireCoroutine(Transform target)
        {
            angleSpreads = new float[config.bulletsCount];
            lastTargetPosition = target.position;
            var distance = Vector3.Distance(target.position, transform.position);

            for (int i = 0; i < config.bulletsCount; i++)
            {
                float angleModifier = i - config.bulletsCount / 2;
                angleSpreads[i] = config.angle * angleModifier;
            }

            for (int i = 0; i < config.bulletsCount; i++)
            {
                var bullet = spawner.Spawn(config.bulletToShoot, shootingAnchor.transform.position) as Bullet;
                posToShootAt = transform.position + Quaternion.AngleAxis(angleSpreads[i], transform.up) * transform.forward * distance;

                if (target != null && bullet.RefreshTargetPositon)
                    bullet.Initialize(target);
                else
                    bullet.Initialize(posToShootAt);

                yield return new WaitForSeconds(config.burstDelay);
            }
        }
    }
}