using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private const string CollisionAnimation = "Stumble Backwards";

        [Header("Game Objects")]
        [SerializeField] private Animator _characterAnimator;
        [SerializeField] private GameObject _camera;
        
        [Header("Audio Sources")]
        [SerializeField] private AudioSource _crashSound;

        public void OnCrash()
        {
            StopPlayerMovement();
            TriggerPlayerCollisionAnimation();
            PlayCrashSoundEffect();
            TriggerShakingCameraAnimation();
            TriggerEndRunRoutine();
        }
        
        private void PlayCrashSoundEffect()
        {
            _crashSound.Play();
        }

        private void StopPlayerMovement()
        {
            this.gameObject.GetComponent<PlayerMoveController>().enabled = false;
            this.gameObject.GetComponent<PlayerMovementController>().enabled = false;
        }

        private void TriggerPlayerCollisionAnimation()
        {
            _characterAnimator.Play(CollisionAnimation);
        }
        
        private void TriggerShakingCameraAnimation()
        { 
            _camera.GetComponent<Animator>().enabled = true;
        }

        private void TriggerEndRunRoutine()
        {
            GameManager.Instance.EndGameSession();
        }
    }
}
