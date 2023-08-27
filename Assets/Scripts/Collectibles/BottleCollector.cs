using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCollector : MonoBehaviour
{
    private const short SINGLE_BOTTLE_SCORE = 1;
    [SerializeField] AudioSource bottleOpenFX;
    private void OnTriggerEnter(Collider other) {
        bottleOpenFX.Play();
        CollectiblesController.addToScore(SINGLE_BOTTLE_SCORE);
        this.gameObject.SetActive(false);
    }
}
