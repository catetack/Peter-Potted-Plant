using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject Cube;

    InputSystem_Actions inputAction;//
    public float speed;
    void Start()
    {
        inputAction = new InputSystem_Actions();
        inputAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        //read value from joysticks
        Debug.Log(message: inputAction.Player.Move.ReadValue<Vector2>());
        Vector3 translate = new Vector3(inputAction.Player.Move.ReadValue<Vector2>().x, 0, inputAction.Player.Move.ReadValue<Vector2>().y);
        Cube.transform.Translate(speed*translate);
    }
}
