using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        private const float FadeInAnimationLength = 1.35f;
        private readonly List<string> _countDownWords = new List<string> { "3", "2", "1", "GO!" };
        public float AnimationDuration => 5.4f;
        
        [SerializeField] private TextMeshProUGUI _bottleCountDisplay;
        [SerializeField] private TextMeshProUGUI _bottleEndCountDisplay;
        [SerializeField] private TextMeshProUGUI _distanceDisplay;
        [SerializeField] private TextMeshProUGUI _distanceEndDisplay;
        [SerializeField] private TextMeshProUGUI _countdownLabel;
        
        [SerializeField] private GameObject _countdown;

        [SerializeField] private AnimationClip _countAnimationClip;
        [SerializeField] private Animator _countdownAnimator;
        
        [SerializeField] private AudioSource _readySound;
        [SerializeField] private AudioSource _goSound;

        public static UIManager Instance { get; private set; }

        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                
                _countdown.gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetDistance(int distance)
        {
            _distanceDisplay.text = distance.ToString();
        }
        
        public void SetScore(int score)
        {
            _bottleCountDisplay.text = score.ToString();
        }
        
        public void SetEndScreenValues(int distance, int score)
        {
            _bottleEndCountDisplay.text = score.ToString();
            _distanceEndDisplay.text = distance.ToString();
        }
        
        public IEnumerator PlayCountdownAnimation()
        {
            _countdown.gameObject.SetActive(true);
            string lastWord = _countDownWords.Last();
            
            yield return new WaitForSeconds(FadeInAnimationLength);
            ResetCountdownAnimation();
            
            foreach (string word in _countDownWords)
            {
                bool isLastWord = !word.Equals(lastWord);
            
                _countdownLabel.text = word;
                _countdownAnimator.Play(_countAnimationClip.name);
                PlayCountingSoundEffect(isLastWord);
                yield return new WaitForSeconds(_countAnimationClip.length);

                if (!word.Equals(lastWord))
                {
                    ResetCountdownAnimation();
                }
            }

            _countdown.gameObject.SetActive(false);
        }

        private void ResetCountdownAnimation()
        {
            _countdownAnimator.Rebind(); // Reset the animation state
            _countdownAnimator.Update(0f); // Reset to the start of the animation
        }

        private void PlayCountingSoundEffect(bool isEndCounting)
        {
            if (!isEndCounting)
            {
                _readySound.Play();
            }
            else
            {
                _goSound.Play();
            }
        }
    }
}
