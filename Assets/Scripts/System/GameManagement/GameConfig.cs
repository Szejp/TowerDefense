using UnityEngine;

namespace AFSInterview.System.GameManagement
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Settings")] 
        public Vector2 boundsMin;
        public Vector2 boundsMax;
        public float enemySpawnRate;
    }
}