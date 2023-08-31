using Managers;
using UnityEngine;

namespace Environment
{
    public class CollisionController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            DisableCollider();
            GameManager.Instance.PlayerCrashIntoObstacle();
        }

        private void DisableCollider()
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
