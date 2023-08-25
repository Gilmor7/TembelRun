using Environment;
using UnityEngine;

namespace Player
{
    public class PlayerMoveController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform _transform;
        
        [Header("Movement Properties")]
        [SerializeField] private float _verticaldSpeed = 3;
        [SerializeField] private float _horizontaldSpeed = 4;
        [SerializeField] public static bool canMove = false;
    
        private void Update()
        {
            MoveForward();

            if (!canMove)
            {
                return;
            }
            
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (_transform.position.x > TrackBoundary.leftSide)
                {
                    MoveHorizontally(EDirection.Left);
                }
            }
            
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (_transform.position.x < TrackBoundary.rightSide)
                {
                    MoveHorizontally(EDirection.Right);
                }
            }
        }

        private void MoveForward()
        {
            Vector3 newPosition = Vector3.forward * (_verticaldSpeed * Time.deltaTime);
            _transform.Translate(newPosition, Space.World);
        }

        private void MoveHorizontally(EDirection iDirection)
        {
            float speed = _horizontaldSpeed * (int)iDirection;
            Vector3 newPosition = Vector3.right * (speed * Time.deltaTime);
        
            _transform.Translate(newPosition);
        }

        private enum EDirection
        {
            Right = 1,
            Left = -1
        }
    }
}
