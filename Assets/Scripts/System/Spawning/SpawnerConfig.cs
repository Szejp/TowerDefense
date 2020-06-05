using AFSInterview.GameElements;
using UnityEngine;

namespace AFSInterview.System.Spawning
{
    [CreateAssetMenu(fileName = "SpawnerConfig", menuName = "Game/Spawning/SpawnerConfig")]
    public class SpawnerConfig : ScriptableObject
    {
        [Header("Prefabs")] 
        public Enemy enemyPrefab;
        public SimpleTower towerPrefab;
        public SimpleTower otherTowerPrefab;
    }
}