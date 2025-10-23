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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActions = new InputSystem_Actions(); //create the instance for the controlls
        inputActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        playerInput();

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
        player.transform.Rotate(0, 0, speed);

    }
    
    void playerInput()
    {

        controllerInput = inputActions.Player.Head.ReadValue<Vector2>();
        headTorque = -controllerInput.x;

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
}
