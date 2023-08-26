using Player;
using UnityEngine;

namespace Environment
{
    public class CollisionController : MonoBehaviour
    {
        private const string CollisionAnimationName = "Stumble Backwards";
        
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _characterModel;
        
        private void OnTriggerEnter(Collider other)
        {
            StopPlayerMovement();
            PlayPlayerCollisionAnimation();
        }

        private void StopPlayerMovement()
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            _player.GetComponent<PlayerMoveController>().enabled = false;
        }

        private void PlayPlayerCollisionAnimation()
        {
            Animator playerAnimator = _characterModel.GetComponent<Animator>();
            playerAnimator.Play(CollisionAnimationName);
        }
    }
}
