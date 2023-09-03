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
        [SerializeField] private Transform _playerTransform;
        private float _startingZPosition; 

        [SerializeField] private PlayerController _playerController;
        
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
            if (Instance == null) // Implemented as singleton only in the scope of the game scene
            {
                Instance = this;
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
            _startingZPosition = _playerTransform.position.z;
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
            _playerController.OnCrash();
        }

        public void CollectBottle(Bottle bottle) {
            _bottleCount += SingleBottleScore;
            bottle.GetCollected();
            UIManager.Instance.SetBottles(_bottleCount);
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
            _distance = (int) (_playerTransform.position.z - _startingZPosition);
            UIManager.Instance.SetDistance(_distance);
            yield return new WaitForSeconds(_addDistanceDelay);
            _shouldAddDistance = false;
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
            int newScore = CalculateScore(_bottleCount, _distance);
            
            UIManager.Instance.SetEndScreenValues(_distance, _bottleCount, newScore);
            yield return new WaitForSeconds(EndScreenDelay);
            _liveScoreDisplay.SetActive(false);
            _endScreen.SetActive(true);
            yield return new WaitForSeconds(EndScreenDelay);
            _fadeOutScreen.SetActive(true);
            yield return new WaitForSeconds(EndScreenDelay);
            _highScoreData.UpdateHighScore(newScore);
            ResetSession();
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        }
    }
}
