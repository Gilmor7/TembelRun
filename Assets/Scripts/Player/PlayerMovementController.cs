using Environment;
using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        private const string JumpingAnimation = "Jump";
        private const string RunningAnimation = "Drunk Run Forward";

        [Header("Components")] [SerializeField]
        private Transform _characterTransform;

        [SerializeField] private CharacterController _characterController;

        [Header("Movement Properties")] [SerializeField]
        private float _verticaldSpeed = 5;

        [SerializeField] private float _horizontaldSpeed = 4;

        void Update()
        {
            float moveX = GetXDirection() * _horizontaldSpeed;
            float moveZ = 1 * _verticaldSpeed;

            Vector3 move = new Vector3(moveX, 0, moveZ);
            _characterController.Move(move * Time.deltaTime);
        }
    
        // Negative return value represents left direction and positive right direction
        private float GetXDirection()
        {
            float x = 0;

            if(GameManager.Instance.IsPlaying)
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    if (_characterTransform.position.x > TrackBoundary.leftSide)
                    {
                        x = -1;
                    }
                }

                else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    if (_characterTransform.position.x < TrackBoundary.rightSide)
                    {
                        x = 1;
                    }
                }
            }

            return x;
        }
    }
}
