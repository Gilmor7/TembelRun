using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionDestroyer : MonoBehaviour
{
    [SerializeField] Transform player;
    void Update()
    {
        //Debug.Log("Player Z: " + player.position.z + " " + transform.name + " z: " + transform.position.z);
        if (player.position.z - transform.position.z > 50 && transform.name == "GeneratedSection") {
            Destroy(gameObject);
        }
    }
}
