using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    [Header("Animations")]
    [SerializeField] private AnimationClip _countAnimationClip;
    [SerializeField] private Animator _countdownAnimator;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _countdownLabel;
    [SerializeField] private RawImage _fadeInScreen;
    
    [Header("Audio Sources")]
    [SerializeField] private AudioSource _readySound;
    [SerializeField] private AudioSource _goSound;
    
    private readonly List<string> _countDownWords = new List<string> { "3", "2", "1", "GO!" };
    private const float FadeInAnimationLength = 1.35f;
    
    void Start()
    {
        StartCoroutine(Countdown());
    }
    
    private IEnumerator Countdown()
    {
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

        PlayerMoveController.canMove = true;
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