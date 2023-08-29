using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceController : MonoBehaviour
{
    [SerializeField] private GameObject distanceDisplayer;
    [SerializeField] private float addDelay = 0.5f;
    private int distanceCounted;
    private bool isToAddToDistance = false;

    void Update()
    {
        if (!isToAddToDistance) {
            isToAddToDistance = true;
            StartCoroutine(addDistance());
        }
    }

    private IEnumerator addDistance() {
        distanceCounted += 1;
        distanceDisplayer.GetComponent<TextMeshProUGUI>().text = distanceCounted.ToString();
        yield return new WaitForSeconds(addDelay);
        isToAddToDistance = false;
    }
}
