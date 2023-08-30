using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common
{
    [CreateAssetMenu(fileName = "High Scores Data", menuName = "HighScoresManager")]
    public class HighScoreData : ScriptableObject
    {
        [SerializeField] private int _count = 3;
        [SerializeField] private List<int> _highScores = new List<int>(); // Array to store high scores

        public List<int> HighScores => _highScores;

        public void UpdateHighScore(int newScore)
        {
            if (_highScores.Count < 5)
            {
                _highScores.Add(newScore);
            }
            
            else if(newScore > _highScores.Last())
            {
                _highScores.RemoveAt(_count - 1);
                _highScores.Add(newScore);
            }
            
            // Sort in descending order
            _highScores.Sort();
            _highScores.Reverse();
        }
    }
}
