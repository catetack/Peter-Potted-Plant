using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class playerStateManager : MonoBehaviour
{
    public bool isDowned = false;
    public bool isReviving = false;
    public bool isHeavy = false;

    public float health = 100.0f;
    public float arduinoWaterValue;

    Animator peterAnimator;

    public float rotationRatio = 0.0f;//outputs from 0 <- +1||-1 -> 0.  10 is at the top, 0 is at the bottom. The sign indicates direction.
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
        peterAnimator.speed = 1; //make sure peter starts off idle
    }

    // Update is called once per frame
    void Update()
    {
        headPosition = peterHead.transform.position;
        //Debug.Log(arduinoWaterValue);

        //sets animation value to Peter's speed
        peterAnimator.SetFloat("Displacement Speed", displacementSpeed);

        ///if Peter is still, play the idle animation
        if (peterAnimator.GetFloat("Displacement Speed") <= 0.1 && peterAnimator.GetFloat("Displacement Speed") >= -0.1)
        {
            UnityEngine.Debug.Log("Idle");
            peterAnimator.speed = 1;
        }

        //if Peter is moving forward, change his animation speed to match how fast he is moving
        else if (peterAnimator.GetFloat("Displacement Speed") > 0.1)
        {
            UnityEngine.Debug.Log("Forward");
            peterAnimator.speed = 1 + displacementSpeed;
        }

        else if (peterAnimator.GetFloat("Displacement Speed") < -0.1)
        {
            UnityEngine.Debug.Log("Backward");
            peterAnimator.speed = 1 + Math.Abs(displacementSpeed);
        }

        //UnityEngine.Debug.Log("displacement speed is " + peterAnimator.GetFloat("Displacement Speed"));
        //UnityEngine.Debug.Log("animation speed is " + peterAnimator.speed);
    }
}
