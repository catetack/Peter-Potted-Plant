using System;
using UnityEngine;

public class rigDisplacement : MonoBehaviour
{
    public GameObject player;

    InputSystem_Actions inputActions;

    Vector2 controllerInput;
    float legsThrottle;
    float footSpeedModifier = 0.5f;
    float acceleration;
    float airFriction = 0.98f;

    //how do i make the value not editable in the inspector but still public?
    public float speed;
    float footSpeed;
    float footSpeedLimit = 1.5f;
    public float speedLimit = 2.5f;

    rigRotation rigRotScript;  
    Transform childTransform;
    float rigRot;
    float rotationClamp;
    public float tiltToSpeedRatio = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();

        childTransform = transform.Find("Rotation");
        if (childTransform != null)
        {
            rigRotScript = childTransform.GetComponent<rigRotation>();
            if (rigRotScript == null)
            {
                Debug.LogError("rigRotation script not found on 'Rotation' child.");
            }
        }
        else
        {
            Debug.LogError("'Rotation' child GameObject not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Rotation Value From rigRotation       -->      rigRotScript.rotationRatio      <--

        controllerInput = inputActions.Player.Legs.ReadValue<Vector2>();
        legsThrottle = controllerInput.x;
        acceleration = legsThrottle * footSpeedModifier;
        
        //reset player. todo later
        if (inputActions.Player.Burst.ReadValue<float>() >= 1.0f)
        {
            acceleration = 0.0f;
            speed += 0.0f;
        }
        // //acceleration = (legsThrottle + tiltToSpeedRatio) * speedModifier;
        // if (acceleration < 0)
        // {
        //     acceleration += 0.01f; // simulate friction when decelerating
        // }
        // else
        // {
        //     acceleration -= 0.01f; // simulate friction when accelerating
        // }

        footSpeed = acceleration * Time.deltaTime;
        //friction in air function
        if (Math.Abs(legsThrottle) < 0.1f)
        {
            speed *= airFriction;
            Debug.Log("air friction applied");
        }
        //speed limit for speed gained from legs
        if (footSpeed > footSpeedLimit) footSpeed = footSpeedLimit;
        if (speed > speedLimit) speed = speedLimit;
        speed += footSpeed;
        
        player.transform.Translate(speed, 0, 0);
    }
}
