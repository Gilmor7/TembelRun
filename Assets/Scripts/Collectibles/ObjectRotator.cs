using UnityEngine;

namespace Collectibles
{
    public class ObjectRotator : MonoBehaviour
    {
        [SerializeField] private int _rotationSpeed = 3;

        void Update()
        {
            transform.Rotate(0f, _rotationSpeed, 0f, Space.World);        
        }
    }
}
