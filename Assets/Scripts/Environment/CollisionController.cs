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
            PlayThePlayerCollisionAnimation();
            PlayCrashSoundEffect();
            PlayShakingCameraAnimation();
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

        private void PlayThePlayerCollisionAnimation()
        {
            Animator playerAnimator = _characterModel.GetComponent<Animator>();
            playerAnimator.Play(CollisionAnimation);
        }
        
        private void PlayShakingCameraAnimation()
        { 
            _camera.GetComponent<Animator>().enabled = true;
        }
    }
}
