using UnityEngine;

namespace AFSInterview.System.Spawning
{
    [CreateAssetMenu(fileName = "SpawnerConfig", menuName = "Game/Spawning/SpawnerConfig")]
    public class SpawnerConfig : ScriptableObject
    {
        [Header("Prefabs")] 
        public GameObject enemyPrefab;
        public GameObject towerPrefab;
    }
}