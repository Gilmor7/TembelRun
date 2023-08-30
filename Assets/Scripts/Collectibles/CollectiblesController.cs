using TMPro;
using UnityEngine;

namespace Collectibles
{
    public class CollectiblesController : MonoBehaviour {
        private static int bottleCount = 0;
        [SerializeField] private GameObject bottleCountDisplay;

        void Update() {
            bottleCountDisplay.GetComponent<TextMeshProUGUI>().text = bottleCount.ToString();
        }

        public static void addToScore(int score) {
            bottleCount += score;
        }
        
        public static int GetBottleCount() {
            return bottleCount;
        }
    }
}
