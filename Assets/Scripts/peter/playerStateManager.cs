using System;
using UnityEngine;

public class playerStateManager : MonoBehaviour
{
    public bool isDowned = false;
    public bool isReviving = false;
    public bool isHeavy = true;

    public float rotationRatio = 0.0f;//outputs from 0 <- -10||10 -> 0.  10 is at the top, 0 is at the bottom. The sign indicates direction.
    public float rotationSpeed = 0.0f;

    public float displacementSpeed = 0.0f;
    public float arduinoWaterLevel = 0.0f;

    public float health = 100.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isHeavy = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
