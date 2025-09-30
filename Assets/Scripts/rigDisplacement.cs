using System;
using UnityEngine;

public class rigDisplacement : MonoBehaviour
{
    bool KEYBOARD = true;
    public GameObject player;

    InputSystem_Actions inputActions;

    Vector2 controllerInput;
    float legsThrottle;
    public float baseSpeedMultiplier;
    public float airFriction = 0.99f;

    public float speed;

    rigRotation rigRotScript;
    Transform childTransform;
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


    void FixedUpdate()
    {
        if (Math.Abs(legsThrottle) < 0.1f || Math.Abs(legsThrottle) > -0.1f)
        {
            speed *= airFriction;
            Debug.Log("air friction applied");
        }
    }   
    // Update is called once per frame
    void Update()
    {

        playerInput();

        //speed limit for speed gained from legs
        speed += (baseSpeedMultiplier * legsThrottle) * Time.deltaTime;

        player.transform.Translate(speed, 0, 0);
    }

    void playerInput()
    {

        controllerInput = inputActions.Player.Legs.ReadValue<Vector2>();
        legsThrottle = controllerInput.x;

        if (!KEYBOARD) return;
        if (Input.GetKey(KeyCode.D))
        {

            legsThrottle = 1.0f;
        }
        if (Input.GetKey(KeyCode.A))
        {

            legsThrottle = -1.0f;
        }
    }
}
