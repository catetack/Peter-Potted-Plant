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

    rigRotation Rotation;
    Transform childTransform;

    void Start()
    {
        baseSpeedConstant = 25.0f;
        frictionConstant = 1.0f;
        displacementSpeed = 0.0f;

        inputActions = new InputSystem_Actions();
        inputActions.Enable();

        childTransform = transform.Find("Rotation");
        if (childTransform != null)
        {
            Rotation = childTransform.GetComponent<rigRotation>();
            if (Rotation == null)
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
        playerInput();
        devTools();
        playerMovement();

        player.transform.Translate(displacementSpeed, 0, 0);
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
        float rotation = Rotation.rotationRatio * 10.0f;

        if (Math.Abs(legsThrottle) > 0.1f)
        {
            displacementSpeed = (baseSpeedConstant * legsThrottle) * Time.deltaTime; // temporal value
        }

        frictionExpression = 1 - frictionConstant * Math.Abs(rotation) * Time.deltaTime;
        displacementSpeed *= frictionExpression; // temporal value

    }
    void devTools()
    {
        if (!DEBUG) return;

        if (Input.GetKey(KeyCode.Keypad0))
        {
            Debug.Log("speed: " + displacementSpeed);
            Debug.Log("frictionConstant: " + frictionConstant);
            Debug.Log("baseSpeedConstant: " + baseSpeedConstant);
            Debug.Log("legsThrottle: " + legsThrottle);
            Debug.Log("rotationRatio: " + Rotation.rotationRatio);
        }
        if (Input.GetKey(KeyCode.Keypad1))
        {
            frictionConstant -= 1.0f;
            Debug.Log(frictionConstant);
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            frictionConstant += 1.0f;
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
