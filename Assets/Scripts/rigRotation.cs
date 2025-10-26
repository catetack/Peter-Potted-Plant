using System;
using Unity.Mathematics;
using UnityEngine;

public class rigRotation : MonoBehaviour
{
    public GameObject player;
    bool KEYBOARD = true;

    InputSystem_Actions inputActions; // variable reference but there is no object in memory

    Vector2 controllerInput;
    float headTorque;
    float speedModifier = 300.0f;
    float speed;

    float rigRot;
    float rotationClamp;
    public float rotationRatio;

    rigDisplacement Displacement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Displacement = GetComponentInParent<rigDisplacement>();
        if (Displacement == null)
        {
            Debug.LogError("rigDisplacement script not found on 'Displacement' parent.");
        }
        inputActions = new InputSystem_Actions(); //create the instance for the controlls
        inputActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        playerInput();
        playerRotations();
        movementRotations();
        gravityRotation();

        player.transform.Rotate(0, 0, speed);
    }

    void playerInput()
    {

        controllerInput = inputActions.Player.Head.ReadValue<Vector2>();
        if (Math.Abs(controllerInput.x) > 0.1f)
        {
            headTorque = -controllerInput.x;
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
        speed = headTorque * speedModifier * Time.deltaTime;

        //rig rotation function:
        rigRot = transform.eulerAngles.z;
        rotationClamp = (rigRot > 180f) ? rigRot - 360f : rigRot;
        rotationRatio = 1 - Math.Abs(rotationClamp / 180f);
        if (Math.Abs(rotationClamp) != 0.0f)
        {
            rotationRatio *= rotationClamp / Math.Abs(rotationClamp);
        }
        else {
            rotationRatio = 0.0f;
        }

    }
    void movementRotations()
    {
        float movementTorque = 250.0f;
        //currently the at wich the head moves from the player speed is always the same, making it very difficult to keep the head stable.
        //speed at which the head moves should increase after the head passes +-20° from the top
        speed += Displacement.displacementSpeed * movementTorque * Time.deltaTime;
    }
    void gravityRotation()
    {
        float gravityTorque = 2.0f;
        //simulate gravity pulling the head downwards when not moving
        speed += rotationClamp * gravityTorque * Time.deltaTime;
    }
}
