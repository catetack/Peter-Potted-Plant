using System;
using UnityEngine;

public class rigDisplacement : MonoBehaviour
{
    // Development tools
    const bool KEYBOARD = true;
    const bool DEBUG = true;

    public GameObject player;
    InputSystem_Actions inputActions;
    Vector2 controllerInput;
    float legsThrottle;

    float baseSpeedConstant;
    float frictionConstant; // multiplier for the friction expression
    public float displacementSpeed;

    playerStateManager PlayerState;
    void Start()
    {
        baseSpeedConstant = 25.0f;
        frictionConstant = 1.0f;
        displacementSpeed = 0.0f;
        assignObjects();
        
        
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        playerInput();
        devTools();
        if (PlayerState.isDowned)
        {
            float targetSpeed = 0.0f;
            float blend = Mathf.Pow(0.5f, 25.0f * Time.deltaTime);
            displacementSpeed = Mathf.Lerp(targetSpeed, displacementSpeed, blend);//smooth stop when downed
        }
        else
        {
            playerMovement();
        }

        PlayerState.displacementSpeed = displacementSpeed;
        player.transform.Translate(displacementSpeed, 0, 0);
    }

    void assignObjects()
    {
        PlayerState = GetComponentInParent<playerStateManager>();
        if (PlayerState == null)
        {
            Debug.LogError("playerStateManager script not found on 'PlayerState' parent.");
        }
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

    void playerMovement()
    {
        
        float frictionExpression;
        float rotation = PlayerState.rotationRatio * 10.0f;

        if (Math.Abs(legsThrottle) > 0.1f)
        {
            displacementSpeed = (baseSpeedConstant * legsThrottle) * Time.deltaTime;
        }

        frictionExpression = frictionConstant * Math.Abs(rotation);

        float targetSpeed = 0.0f;
        float blend = Mathf.Pow(0.5f, frictionExpression * Time.deltaTime);
        displacementSpeed = Mathf.Lerp(targetSpeed, displacementSpeed, blend);
        //displacementSpeed *= frictionExpression; //deceleration due to friction

    }
    void devTools()
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
        if (Input.GetKey(KeyCode.Keypad3))
        {
            CreateLagSpike();
        }
    }
    void CreateLagSpike()
    {
        for (int i = 0; i < 10000000; i++)
        {
            Vector3 lag = new Vector3(MathF.Sin(i), MathF.Cos(i), MathF.Tan(i));
        }
    }
}
