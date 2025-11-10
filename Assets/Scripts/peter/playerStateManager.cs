using System;
using UnityEngine;

public class playerStateManager : MonoBehaviour
{
    public bool isDowned = false;
    public bool isReviving = false;
    public bool isHeavy = false;
    public float health = 100.0f;

    Animator peterAnimator;


    public float rotationRatio = 0.0f;//outputs from 0 <- -10||10 -> 0.  10 is at the top, 0 is at the bottom. The sign indicates direction.
    public float rotationSpeed = 0.0f;

    public float displacementSpeed = 0.0f;
    public float arduinoWaterLevel = 0.0f;

    GameObject peterHead;
    public Vector3 headPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        peterHead = GameObject.Find("peterHead");
        isHeavy = false;
        peterAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        headPosition = peterHead.transform.position;
        peterAnimator.SetFloat("speed", displacementSpeed);

        //Debug.Log(legsThrottle);
        Debug.Log("animation speed is " + peterAnimator.GetFloat("speed"));
    }
}
