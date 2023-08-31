using System.Collections;
using Collectibles;
using Common;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private const short SingleBottleScore = 1;
        private const float EndScreenDelay = 3f;
        
        [SerializeField] private float _addDistanceDelay = 0.5f;

        [SerializeField] private PlayerController _player;
        
        [SerializeField] private GameObject _liveScoreDisplay;
        [SerializeField] private GameObject _endScreen;
        [SerializeField] private GameObject _fadeOutScreen;
        [SerializeField] private HighScoreData _highScoreData;
        
        [SerializeField] private AudioSource _backgroundMusic;

        private int _bottleCount;
        private int _distance;
        private bool _shouldAddDistance = false;

        public bool IsPlaying { get; private set; } = false;
        public static GameManager Instance { get; private set; }

        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            StartGame();
        }
        
        void Update()
        {
            if (IsPlaying)
            {
                TrackPlayerDistance();
            }
        }

        private void StartGame()
        {
            StartCoroutine(UIManager.Instance.PlayCountdownAnimation());
            StartCoroutine(WaitForAnimationAndStart(UIManager.Instance.AnimationDuration));
        }

        private void StartActualGame()
        {
            _backgroundMusic.Play();
            IsPlaying = true;
        }

        public void EndGameSession()
        {
            IsPlaying = false;
            StartCoroutine(EndGameRoutine());
        }
        
        private void ResetSession()
        {
            _bottleCount = 0;
            _distance = 0;
        }

        public void PlayerCrashIntoObstacle()
        {
            _player.OnCrash();
        }

        public void CollectBottle(Bottle bottle) {
            _bottleCount += SingleBottleScore;
            bottle.GetCollected();
            UIManager.Instance.SetScore(_bottleCount);
        }

        private void TrackPlayerDistance()
        {
            if (_shouldAddDistance == false) 
            {
                _shouldAddDistance = true;
                StartCoroutine(AddDistanceRoutine());
            }
        }

        private IEnumerator AddDistanceRoutine() {
            _distance += 1;
            UIManager.Instance.SetDistance(_distance);
            yield return new WaitForSeconds(_addDistanceDelay);
            _shouldAddDistance = false;
        }
        
        private void UpdateHighScores(int bottleCount, int distance)
        {
            int newScore = CalculateScore(bottleCount, distance);
            _highScoreData.UpdateHighScore(newScore);
        }

        private int CalculateScore(int bottleCount, int distance)
        {
            return bottleCount + distance;
        }
        
        private IEnumerator WaitForAnimationAndStart(float duration)
        {
            yield return new WaitForSeconds(duration);
            StartActualGame();
        }
        
        private IEnumerator EndGameRoutine()
        {
            UIManager.Instance.SetEndScreenValues(_distance, _bottleCount);
            yield return new WaitForSeconds(EndScreenDelay);
            _liveScoreDisplay.SetActive(false);
            _endScreen.SetActive(true);
            yield return new WaitForSeconds(EndScreenDelay);
            _fadeOutScreen.SetActive(true);
            yield return new WaitForSeconds(EndScreenDelay);
            UpdateHighScores(_bottleCount, _distance);
            ResetSession();
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        }
    }
}
