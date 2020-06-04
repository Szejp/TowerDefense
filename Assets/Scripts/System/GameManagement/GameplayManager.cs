using AFSInterview.System.Spawning;
using UnityEngine;

namespace AFSInterview.System.GameManagement
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] GameConfig config;
        [SerializeField] Spawner spawner;

        private void Awake()
        {
           spawner.SetBounds(config.boundsMin, config.boundsMax);
           spawner.SetEnemySpawnRate(config.enemySpawnRate);
        }
    }
}