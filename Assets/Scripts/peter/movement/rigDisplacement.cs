using System;
using UnityEngine;

public class rigDisplacement : MonoBehaviour
{
    //Debug
    const bool KEYBOARD = true;
    const bool DEBUG = true;

    //References
    playerStateManager PlayerState;
    public GameObject player;
    Rigidbody2D rb;

    InputSystem_Actions inputActions;
    Vector2 leftStickInput;
    float legsThrottle;

    //Lateral movement
    float baseSpeedConstant;
    float frictionConstant;
    float speedFromTilt;
    float frictionExpression;
    float targetDisplacementSpeed;
    public float displacementSpeed;

    //Vertical movement
    float maxFallingSpeed;

    void Start()
    {

        AssignObjects();
    }
    void AssignObjects()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new InputSystem_Actions();
        inputActions.Player.Legs.Enable();

        baseSpeedConstant = 25.0f;
        frictionConstant = 10.0f;
        displacementSpeed = 0.0f;
        maxFallingSpeed = 0.1f;
        targetDisplacementSpeed = 0.0f;

        PlayerState = GetComponentInParent<playerStateManager>();
        if (PlayerState == null) Debug.LogError("playerStateManager script not found on 'PlayerState' parent.");
    }

    // Update is called once per frame
    void Update()
    {

        PlayerInput();
        DevTools();
        InStateMovement();
        //FallingSpeed();
        player.transform.Translate(displacementSpeed, 0, 0);
    }

    private void FallingSpeed()
    {
        //Limit the max falling speed
        if (rb.linearVelocityY < 0f)
        {

            if (Mathf.Abs(rb.linearVelocityY) < maxFallingSpeed)
            {

            }
            else
            {

                rb.linearVelocityY = -maxFallingSpeed;
            }
        }
        PlayerState.displacementSpeed = displacementSpeed;
    }

    private void InStateMovement()
    {
        
        if (PlayerState.isDowned || PlayerState.isReviving) DisplacementBreaks();
        else PlayerMovement();
    }

    void PlayerInput()
    {
        
        ControllerInput();
        if (KEYBOARD) KeyboardInput();
    }

    private void KeyboardInput()
    {

        if (Input.GetKey(KeyCode.D)) legsThrottle = 1.0f;
        if (Input.GetKey(KeyCode.A)) legsThrottle = -1.0f;
    }

    private void ControllerInput()
    {

        leftStickInput = inputActions.Player.Legs.ReadValue<Vector2>();
        legsThrottle = leftStickInput.x;
    }

    void PlayerMovement()
    {

        speedFromTilt = (1.0f - Math.Abs(PlayerState.rotationRatio)) * 50.0f * legsThrottle / Math.Abs(legsThrottle);
        TargetThrottleSpeed();
        MovementSmoothing();
    }

    private void TargetThrottleSpeed()
    {

        if (Math.Abs(legsThrottle) > 0.2f) targetDisplacementSpeed = (baseSpeedConstant * legsThrottle + speedFromTilt) * Time.deltaTime;
        else targetDisplacementSpeed = 0.0f;
    }

    private void MovementSmoothing()
    {

        frictionExpression = frictionConstant * Math.Abs(PlayerState.rotationRatio);
        displacementSpeed = Mathf.Lerp(targetDisplacementSpeed, displacementSpeed, Mathf.Pow(0.5f, frictionExpression * Time.deltaTime));
    }

    void DisplacementBreaks()
    {
        
        displacementSpeed = Mathf.Lerp(0.0f, displacementSpeed, Mathf.Pow(0.5f, baseSpeedConstant * Time.deltaTime));
    }
    void DevTools()
    {
        if (!DEBUG) return;
        if (Input.GetKeyUp(KeyCode.Keypad0))
        {

            Debug.Log("baseSpeedConstant: " + baseSpeedConstant);
            Debug.Log("frictionConstant: " + frictionConstant);
            Debug.Log("legsThrottle: " + legsThrottle);
            Debug.Log("speed: " + displacementSpeed);
            Debug.Log("rotationRatio: " + PlayerState.rotationRatio);
        }
        if (Input.GetKeyUp(KeyCode.Keypad1))
        {

            frictionConstant -= 0.10f;
            Debug.Log(frictionConstant);
        }
        if (Input.GetKeyUp(KeyCode.Keypad2))
        {

            frictionConstant += 0.10f;
            Debug.Log(frictionConstant);
        }
    }
}
