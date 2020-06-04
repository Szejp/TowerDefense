using AFSInterview.System.Spawning;

namespace AFSInterview.UI
{
    public class EnemiesCountLabel : TextMeshLabel
    {
        const string ENEMIES_COUNT_FORMAT = "Enemies: {0}";

        void Awake()
        {
            Spawner.OnEnemiesCountChanged += OnEnemiesCountChangedHandler;
        }

        void OnDestroy()
        {
            Spawner.OnEnemiesCountChanged -= OnEnemiesCountChangedHandler;
        }

        void OnEnemiesCountChangedHandler(int count)
        {
            Text = string.Format(ENEMIES_COUNT_FORMAT, count);
        }
    }
}