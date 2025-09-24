using UnityEngine;

public class rigRotation : MonoBehaviour
{
    public GameObject player;

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
        controllerInput = inputActions.Player.Head.ReadValue<Vector2>();
        headTorque = -controllerInput.x;
        speed = headTorque * speedModifier * Time.deltaTime;

        //rig rotation function:
        rigRot = transform.eulerAngles.z;
        rotationClamp = (rigRot > 180f) ? rigRot - 360f : rigRot;
        rotationRatio = rotationClamp / 90f;

        

        player.transform.Rotate(0, 0, speed);

    }
}
