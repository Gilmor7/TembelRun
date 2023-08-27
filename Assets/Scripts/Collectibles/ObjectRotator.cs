using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    [SerializeField] private int rotationSpeed = 3;

    void Update()
    {
        transform.Rotate(0f, rotationSpeed, 0f, Space.World);        
    }
}
