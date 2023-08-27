using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectiblesController : MonoBehaviour {
    private static int bottleCount = 0;
    [SerializeField] private GameObject bottleCountDisplay;

    void Update() {
        bottleCountDisplay.GetComponent<TextMeshProUGUI>().text = bottleCount.ToString();
    }

    public static void addToScore(int score) {
        bottleCount += score;
    }
}
