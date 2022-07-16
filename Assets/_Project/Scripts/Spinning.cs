using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    public float yRotation;
    private GameObject cube;

    void Awake()
    {
        cube = this.gameObject;
    }

    void Update()
    {
        cube.transform.Rotate(0, yRotation, 0, Space.World);      
    }
}
