using System;
using UnityEngine;

public class rigDisplacement : MonoBehaviour
{
    //Debug
    const bool KEYBOARD = true;
    const bool DEBUG = false;
    const float INPUT_DEADZONE = 0.2f;

    const float PLAYER_SPEED_HEAYY = 2.0f;
    const float PLAYER_SPEED_LIGHT = 15.0f;

    //References
    playerStateManager PlayerState;
    public GameObject player;
    Rigidbody2D rb;

    InputSystem_Actions inputActions;
    Vector2 leftStickInput;
    float legsThrottle;

    //Lateral movement
    public float baseSpeedConstant;
    float frictionConstant;
    float speedFromTilt;
    float tiltSpeedModifier;
    float frictionExpression;
    float targetDisplacementSpeed;
    public float displacementSpeed;

    //Vertical movement
    public float maxFallingSpeed;
    public float maxUpSpeed;

    //Pause Menu
    PauseMenu pm;

    void Start()
    {

        AssignObjects();
        pm=GameObject.Find("Main Camera").GetComponent<PauseMenu>();
    }
    void AssignObjects()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new InputSystem_Actions();
        inputActions.Player.Legs.Enable();

        baseSpeedConstant = 2.0f;
        frictionConstant = 10.0f;
        tiltSpeedModifier = 100.0f;
        displacementSpeed = 0.0f;
        maxFallingSpeed = 80f;
        maxUpSpeed = 1000f;
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
        LightHeavyModeSpeedControl();
        PlayerState.displacementSpeed = displacementSpeed;

        FallingSpeed();

        //Here is the older one
        player.transform.Translate(displacementSpeed, 0, 0);
        //player.transform.Translate(displacementSpeed, rb.linearVelocityY * Time.deltaTime, 0);

        if (pm != null)
        {
            if (!pm.GetmenuKey())
            {
                displacementSpeed = 0.0f;
            }

        }
    }

    private void LightHeavyModeSpeedControl()
    {
        if (PlayerState.isHeavy)
        {
            baseSpeedConstant = PLAYER_SPEED_HEAYY;
        }
        else
        {
            baseSpeedConstant = PLAYER_SPEED_LIGHT;
        }
    }
    private void FallingSpeed()
    {
        //Limit the max falling speed
        Vector2 v = rb.linearVelocity;

        // 限制下落与上升速度
        v.y = Mathf.Clamp(v.y, -maxFallingSpeed, maxUpSpeed);

        rb.linearVelocity = v;
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

        speedFromTilt = baseSpeedConstant + (1.0f - Math.Abs(PlayerState.rotationRatio)) * tiltSpeedModifier;
        TargetThrottleSpeed();
        MovementSmoothing();
    }

    private void TargetThrottleSpeed()
    {

        if (Math.Abs(legsThrottle) > INPUT_DEADZONE) targetDisplacementSpeed = (legsThrottle * speedFromTilt) * Time.deltaTime;
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