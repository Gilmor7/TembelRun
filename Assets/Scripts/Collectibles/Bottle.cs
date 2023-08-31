using Managers;
using UnityEngine;

namespace Collectibles
{
    public class Bottle : MonoBehaviour
    {
        [SerializeField] AudioSource bottleOpenFX;
    
        private void OnTriggerEnter(Collider other) 
        {
            GameManager.Instance.CollectBottle(this);
        }

        public void GetCollected()
        {
            bottleOpenFX.Play();
            this.gameObject.SetActive(false);
        }
    }
}
