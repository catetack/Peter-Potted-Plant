using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public GameObject player;
    public float speed = 0f;

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

        //Vector2 translate = new Vector2(inputActions.Player.Head.ReadValue<Vector3>().x, 0);
        //Debug.Log(inputActions.Player.Legs.ReadValue<Vector2>());
        //inputActions.Player.Head.ReadValue<Vector2>();
        Debug.Log("LEGS: " + inputActions.Player.Legs.ReadValue<Vector2>() + ", " +
        inputActions.Player.Jump.ReadValue<float>() + System.Environment.NewLine +
        "HEAD: " + inputActions.Player.Head.ReadValue<Vector2>() + ", " +
        inputActions.Player.Burst.ReadValue<float>());


        //player.transform.Translate(speed * translate);
    }
}
