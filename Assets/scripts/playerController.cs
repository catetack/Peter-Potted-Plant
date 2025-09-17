using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public GameObject player;

    InputSystem_Actions inputActions; // variable reference but there is no object in memory

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActions = new InputSystem_Actions(); //create the instance for the controlls
        inputActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
        //displays the read value for the keyboard
        Debug.Log("LEGS: " + inputActions.Player.Legs.ReadValue<Vector2>() + ", " +
        inputActions.Player.Jump.ReadValue<float>() + System.Environment.NewLine +
        "HEAD: " + inputActions.Player.Head.ReadValue<Vector2>() + ", " +
        inputActions.Player.Burst.ReadValue<float>());
    }
}
