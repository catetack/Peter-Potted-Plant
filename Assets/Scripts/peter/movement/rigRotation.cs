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

    //input
    InputSystem_Actions inputActions;
    Vector2 yStickInput;
    float rotationClamp;

    //forces
    float movementTorque;
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
        
        raiseTorque = 2.0f;
        gravityTorque = 2.0f;
        lightTorque = 5.0f;
        torqueModifier = 300.0f;
        movementTorque = 250.0f;
        
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
        if (Math.Abs(yStickInput.x) > 0.1f && !PlayerState.isDowned)
        {
            
            headTorque = -yStickInput.x;
        }
        else
        {
            
            headTorque = 0.0f;
        }

        if (!KEYBOARD) return;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            
            headTorque = -1.0f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            
            headTorque = 1.0f;
        }
    }
    void playerRotations()
    {

        normalizeRotation();
        if (Math.Abs(rotationClamp) != 0.0f)
        {

            PlayerState.rotationRatio *= rotationClamp / Math.Abs(rotationClamp);
        }
        else
        {

            PlayerState.rotationRatio = 0.0f;
        }

        rotationSpeed = headTorque * torqueModifier * Time.deltaTime;
    }
    void normalizeRotation()
    {
        
        rotationClamp = (transform.eulerAngles.z > 180f) ? transform.eulerAngles.z - 360f : transform.eulerAngles.z;
        PlayerState.rotationRatio = 1 - Math.Abs(rotationClamp / 180f);
    }
    void movementRotations()
    {
        
        rotationSpeed += PlayerState.displacementSpeed * movementTorque * Time.deltaTime;
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
