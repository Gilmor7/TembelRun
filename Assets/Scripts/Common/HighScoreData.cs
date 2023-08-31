using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common
{
    [CreateAssetMenu(fileName = "HighScoresData", menuName = "High Scores Manager")]
    public class HighScoreData : ScriptableObject
    {
        [SerializeField] private int _maxCount = 3;
        [SerializeField] private List<int> _highScores = new List<int>();

        public List<int> HighScores => _highScores;

        public void UpdateHighScore(int newScore)
        {
            if (_highScores.Count < _maxCount)
            {
                _highScores.Add(newScore);
            }
            
            else if(newScore > _highScores.Last())
            {
                _highScores.RemoveAt(_maxCount - 1);
                _highScores.Add(newScore);
            }
            
            // Sort in descending order
            _highScores.Sort();
            _highScores.Reverse();
        }
    }
}