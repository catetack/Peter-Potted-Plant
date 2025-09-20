using UnityEngine;
using UnityEngine.UIElements;

public class pFeet : MonoBehaviour
{
    public GameObject player;

    InputSystem_Actions inputActions;

    Vector2 controllerInput;
    float legsThrottle;
    float speedModifier = 0.1f;
    float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        inputActions = new InputSystem_Actions();
        inputActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {

        controllerInput = inputActions.Player.Legs.ReadValue<Vector2>();
        legsThrottle = controllerInput.x;
        speed = (legsThrottle /* + tiltToSpeedRatio */ ) * speedModifier;
        player.transform.Translate(speed, 0, 0);
    }
}
