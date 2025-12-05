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
    float targetRotationSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        assignObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
        playerInput();
        
        if (!PlayerState.isDowned && !PlayerState.isHeavy)
        {
            
            playerRotations();
            lightRotation();
        }
        else if (!PlayerState.isDowned && PlayerState.isHeavy)
        {
            
            playerRotations();
            if (PlayerState.isGrounded)
            {
                
                movementRotations();
                gravityRotation();
            }
        }
        else if (PlayerState.isDowned)
        {

            healingBasedRotation();
        }
        ApplyRotationSpeedSmoothing();
        player.transform.Rotate(0, 0, rotationSpeed);
    }
    void assignObjects()
    {

        inputActions = new InputSystem_Actions(); //create the instance for the controlls
        
        //magic numbers
        raiseTorque = 2.0f;
        gravityTorque = 1.9f;
        lightTorque = 5.0f;
        torqueModifier = 225.0f;
        resultingMovementTorque = 0.0f;
        
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
        
        PlayerState.headTorque = headTorque;
        // Keyboard override (if enabled)
        if (KEYBOARD)
        {

            if (Input.GetKey(KeyCode.RightArrow)) headTorque = -1.0f;
            if (Input.GetKey(KeyCode.LeftArrow)) headTorque = 1.0f;
        }
    }
    void playerRotations()
    {

        NormalizeRotation();
        RotationDirection();

        targetRotationSpeed = headTorque * torqueModifier * Time.deltaTime;
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

    void NormalizeRotation()
    {
        
        rotationClamp = (transform.eulerAngles.z > 180f) ? transform.eulerAngles.z - 360f : transform.eulerAngles.z;
        PlayerState.rotationRatio = 1 - Math.Abs(rotationClamp / 180f);
    }
    void movementRotations()
    {
        //reduce effect of movement on rotation when closer to the bottom
        resultingMovementTorque = BASE_MOVEMENT_TORQUE * (PlayerState.rotationRatio*PlayerState.rotationRatio);
        targetRotationSpeed += PlayerState.displacementSpeed * resultingMovementTorque * Time.deltaTime;
    }

    void gravityRotation()
    {
        
        targetRotationSpeed += rotationClamp * gravityTorque * Time.deltaTime;
    }
    void raisedRotation()
    {
        
        targetRotationSpeed -= rotationClamp * raiseTorque * Time.deltaTime;
    }
    void lightRotation()
    {
        
        targetRotationSpeed -= rotationClamp * lightTorque * Time.deltaTime;
    }
    void healingBasedRotation()
    {
        if (PlayerState.isReviving)
        {
            playerRotations();
            raisedRotation();
        }
        else if (!PlayerState.isReviving && PlayerState.health < 99.0f)
        {   
            playerRotations();
            if (PlayerState.health > 0.0f)
            {
                gravityRotation();
            }
        }
        else
        {
            targetRotationSpeed = 0.0f;
            rotationSpeed = 0.0f;
        }
    }
    void ApplyRotationSpeedSmoothing()
    {
        if (Time.timeScale == 0f)
        {
            rotationSpeed = 0f;
            return;
        }
        
        rotationSpeed = Mathf.Lerp(targetRotationSpeed, rotationSpeed, Mathf.Pow(0.5f, 5.0f * Time.deltaTime));
    }   
        
}
