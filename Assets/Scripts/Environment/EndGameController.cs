using System.Collections;
using Collectibles;
using UnityEngine;
using UnityEngine.SceneManagement;
using Common;

namespace Environment
{
    public class EndGameController : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private float _delay = 3f;
        
        [Header("Game Objects")]
        [SerializeField] private GameObject _liveScoreDisplay;
        [SerializeField] private GameObject _endScreen;
        [SerializeField] private GameObject _fadeOutScreen;

        public HighScoreData highScoreData;

        void Start()
        {
            StartCoroutine(EndGameRoutine());
        }

        IEnumerator EndGameRoutine()
        {
            yield return new WaitForSeconds(_delay);
            _liveScoreDisplay.SetActive(false);
            _endScreen.SetActive(true);
            yield return new WaitForSeconds(_delay);
            _fadeOutScreen.SetActive(true);
            yield return new WaitForSeconds(_delay);
            SceneManager.LoadSceneAsync(0);
            UpdateHighScores();
        }

        private void UpdateHighScores()
        {
            int newScore = CollectiblesController.GetBottleCount();
            highScoreData.UpdateHighScore(newScore);
        }
    }
}
