using System;
using Unity.Mathematics;
using UnityEngine;

public class rigRotation : MonoBehaviour
{
    
    //references
    playerStateManager PlayerState;
    public GameObject player;

    //debug
    bool KEYBOARD = true;
    const float INPUT_DEADZONE = 0.2f;

    //input
    InputSystem_Actions inputActions;
    Vector2 yStickInput;
    float rotationClamp;

    //forces
    const float BASE_MOVEMENT_TORQUE = 600.0f;
    float resultingMovementTorque;
    float headTorque;
    float raiseTorque;
    float gravityTorque;
    float lightTorque;
    float torqueModifier;
    float rotationSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        assignObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
        playerInput();
        if (PlayerState.isDowned)
        {
            
            if (PlayerState.isReviving)
            {
             
                playerRotations();
                raisedRotation();
            }
            else
            {
                
                rotationSpeed = 0.0f;
            }
        }
        else if (!PlayerState.isDowned && !PlayerState.isHeavy)
        {
            
            playerRotations();
            lightRotation();
        }
        else if (!PlayerState.isDowned && PlayerState.isHeavy)
        {
            
            playerRotations();
            movementRotations();
            gravityRotation();
        }

        player.transform.Rotate(0, 0, rotationSpeed);
    }
    void assignObjects()
    {

        inputActions = new InputSystem_Actions(); //create the instance for the controlls
        
        //magic numbers
        raiseTorque = 2.0f;
        gravityTorque = 2.0f;
        lightTorque = 5.0f;
        torqueModifier = 300.0f;
        resultingMovementTorque = 0;
        
        inputActions.Enable();

        PlayerState = GetComponentInParent<playerStateManager>();
        if (PlayerState == null)
        {
            
            Debug.LogWarning("playerStateManager script not found on 'PlayerState' parent.");
        }
    }

    void playerInput()
    {
        yStickInput = inputActions.Player.Head.ReadValue<Vector2>();
        
        // Reset torque first
        headTorque = 0.0f;
        
        // Controller input (only when not downed)
        if (Math.Abs(yStickInput.x) > INPUT_DEADZONE && !PlayerState.isDowned)
        {

            headTorque = -yStickInput.x;
        }
        
        // Keyboard override (if enabled)
        if (KEYBOARD)
        {

            if (Input.GetKey(KeyCode.RightArrow)) headTorque = -1.0f;
            if (Input.GetKey(KeyCode.LeftArrow)) headTorque = 1.0f;
        }
    }
    void playerRotations()
    {

        normalizeRotation();
        RotationDirection();

        rotationSpeed = headTorque * torqueModifier * Time.deltaTime;
    }

    private void RotationDirection()
    {
        if (Math.Abs(rotationClamp) != 0.0f)
        {

            PlayerState.rotationRatio *= rotationClamp / Math.Abs(rotationClamp);
        }
        else
        {

            PlayerState.rotationRatio = 0.0f;
        }
    }

    void normalizeRotation()
    {
        
        rotationClamp = (transform.eulerAngles.z > 180f) ? transform.eulerAngles.z - 360f : transform.eulerAngles.z;
        PlayerState.rotationRatio = 1 - Math.Abs(rotationClamp / 180f);
    }
    void movementRotations()
    {
        //reduce effect of movement on rotation when closer to the bottom
        resultingMovementTorque = BASE_MOVEMENT_TORQUE * (PlayerState.rotationRatio*PlayerState.rotationRatio);
        Debug.Log("movmentTorque: " + resultingMovementTorque);
        rotationSpeed += PlayerState.displacementSpeed * resultingMovementTorque * Time.deltaTime;
    }

    void gravityRotation()
    {
        
        rotationSpeed += rotationClamp * gravityTorque * Time.deltaTime;
    }
    void raisedRotation()
    {
        
        rotationSpeed -= rotationClamp * raiseTorque * Time.deltaTime;
    }
    void lightRotation()
    {
        
        rotationSpeed -= rotationClamp * lightTorque * Time.deltaTime;
    }
}
