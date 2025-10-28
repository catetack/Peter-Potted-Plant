using System;
using Unity.Mathematics;
using UnityEngine;

public class rigRotation : MonoBehaviour
{
    public GameObject player;
    bool KEYBOARD = true;

    InputSystem_Actions inputActions; // variable reference but there is no object in memory

    Vector2 yStickInput;
    float headTorque;
    float speedModifier = 300.0f;
    float rotationSpeed;

    float rigRot;
    float rotationClamp;
    playerStateManager PlayerState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        assignObjects();
        
        inputActions = new InputSystem_Actions(); //create the instance for the controlls
        inputActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        playerInput();
        if (PlayerState.isDowned)
        {
            rotationSpeed = 0.0f;
        }
        else if (!PlayerState.isDowned && !PlayerState.isHeavy)
        {
            playerRotations();
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
        PlayerState = GetComponentInParent<playerStateManager>();
        if (PlayerState == null)
        {
            Debug.LogError("playerStateManager script not found on 'PlayerState' parent.");
        }
    }

    void playerInput()
    {

        yStickInput = inputActions.Player.Head.ReadValue<Vector2>();
        if (Math.Abs(yStickInput.x) > 0.1f)
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
        rotationSpeed = headTorque * speedModifier * Time.deltaTime;
        //float blend = Mathf.Pow(0.5f, 5.0f * Time.deltaTime);
        //rotationSpeed = Mathf.Lerp(targetSpeed, rotationSpeed, blend);

        //rig rotation function:
        rigRot = transform.eulerAngles.z;
        rotationClamp = (rigRot > 180f) ? rigRot - 360f : rigRot;
        PlayerState.rotationRatio = 1 - Math.Abs(rotationClamp / 180f);
        if (Math.Abs(rotationClamp) != 0.0f)
        {
            PlayerState.rotationRatio *= rotationClamp / Math.Abs(rotationClamp);
        }
        else {
            PlayerState.rotationRatio = 0.0f;
        }

    }
    void movementRotations()
    {
        float movementTorque = 250.0f;
        //currently the at wich the head moves from the player speed is always the same, making it very difficult to keep the head stable.
        //speed at which the head moves should increase after the head passes +-20° from the top
        rotationSpeed += PlayerState.displacementSpeed * movementTorque * Time.deltaTime;
    }
    void gravityRotation()
    {
        float gravityTorque = 3.0f;
        //simulate gravity pulling the head downwards when not moving
        rotationSpeed += rotationClamp * gravityTorque * Time.deltaTime;
    }

}
