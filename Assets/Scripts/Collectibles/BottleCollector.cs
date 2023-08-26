using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCollector : MonoBehaviour
{
    [SerializeField] AudioSource bottleOpenFX;
    private void OnTriggerEnter(Collider other) {
        bottleOpenFX.Play();
        this.gameObject.SetActive(false);
    }
}
