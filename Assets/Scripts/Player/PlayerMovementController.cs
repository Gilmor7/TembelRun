using Environment;
using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        private const string JumpingAnimationTrigger = "JumpTrigger";

        [Header("Components")] 
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _characterTransform;
        [SerializeField] private Transform _groundCheckTransform;
        [SerializeField] private Animator _modelAnimator;

        [Header("Movement Properties")] 
        [SerializeField] private float _verticaldSpeed = 5;
        [SerializeField] private float _horizontaldSpeed = 4;

        [Header("Jumping Properties")] 
        [SerializeField] private float _jumpHeight = 1.2f;
        [SerializeField] private float _groundCheckDistance = 0.4f;
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private LayerMask _groundLayer;

        private Vector3 _velocity;
        private bool _isGrounded = true;

        void Update()
        {
            float moveX = GetXDirection() * _horizontaldSpeed;
            float moveZ = 1 * _verticaldSpeed;
            
            Vector3 move = new Vector3(moveX, 0, moveZ);
            _characterController.Move(move * Time.deltaTime);

            _isGrounded = CheckIfGrounded();
            if (Input.GetButtonDown("Jump") && GameManager.Instance.IsPlaying && _isGrounded)
            {
                Jump();
            }
            
            ApplyGravity();
            _characterController.Move(_velocity * Time.deltaTime);
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

        private bool CheckIfGrounded()
        {
            return Physics.CheckSphere(_groundCheckTransform.position, _groundCheckDistance, _groundLayer);
        }

        private void ApplyGravity()
        {
            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            _velocity.y += _gravity * Time.deltaTime;
        }

        private void Jump()
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            _modelAnimator.SetTrigger(JumpingAnimationTrigger);
        }
    }
}
