using AFSInterview.GameElements;
using UnityEngine;

namespace AFSInterview.UI
{
    public class ScoreManager : MonoBehaviour
    {
        const string SCORE_TEXT_FORMAT = "Score: {0}";
        
        [SerializeField] TextMeshLabel scoreLabel;
        
        private int score;

        public int Score
        {
            get => score;
            set
            {
                score = value;
                scoreLabel.Text = string.Format(SCORE_TEXT_FORMAT, score.ToString());
            }
        }

        private void Awake()
        {
            Enemy.OnEnemyDied += OnEnemyDiedHandler;
        }

        private void OnDestroy()
        {
            Enemy.OnEnemyDied -= OnEnemyDiedHandler;
        }

        private void OnEnemyDiedHandler(Enemy enemy)
        {
            Score++;
        }
    }
}