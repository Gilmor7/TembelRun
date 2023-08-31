using Managers;
using Player;
using UnityEngine;

namespace Environment
{
    public class CollisionController : MonoBehaviour
    {
        private const string CollisionAnimation = "Stumble Backwards";

        [Header("Game Objects")]
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _characterModel;
        [SerializeField] private GameObject _camera;
        
        [Header("Audio Sources")]
        [SerializeField] private AudioSource _crashSound;

        
        
        private void OnTriggerEnter(Collider other)
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
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            _player.GetComponent<PlayerMoveController>().enabled = false;
        }

        private void TriggerPlayerCollisionAnimation()
        {
            Animator playerAnimator = _characterModel.GetComponent<Animator>();
            playerAnimator.Play(CollisionAnimation);
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
