using UnityEngine;

namespace Environment
{
    public class SectionDestroyer : MonoBehaviour
    {
        [SerializeField] Transform _playerTransform;
        void Update()
        {
            if (_playerTransform.position.z - transform.position.z > 50 && transform.name == "GeneratedSection") {
                Destroy(gameObject);
            }
        }
    }
}
