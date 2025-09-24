using UnityEngine;

public class rigRotation : MonoBehaviour
{
    public GameObject player;

    InputSystem_Actions inputActions; // variable reference but there is no object in memory

    Vector2 controllerInput;
    float headTorque;
    public float speedModifier = 0.1f;
    float speed;
    public float zRotation;

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
        headTorque = -controllerInput.x;//invert the input to match the direction of rotation
        speed = headTorque * speedModifier;
        player.transform.Rotate(0, 0, speed);

        zRotation = player.transform.eulerAngles.z;
    }
}
