using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject Cube;

    InputSystem_Actions inputAction;//
    void Start()
    {
        inputAction = new InputSystem_Actions();
        inputAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        //read value from joysticks
        Debug.Log(inputAction.Player.Move.ReadValue<Vector2>());
    }
}
