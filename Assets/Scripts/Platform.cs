using UnityEngine;

public class Platform : MonoBehaviour
{
    bool isCrossing=false;

    //Here is how to use it: when the effector is upwards, you can jump across the platform and stand on it.
    //To jump off, change the direction, more specifically change the degree.
    PlatformEffector2D PlatformEffector;
    InputSystem_Actions inputActions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
        PlatformEffector = GetComponent<PlatformEffector2D>();
        //inputActions.Player.Legs.started+= ctx=>isCrossing = true;//When player push joystick down.
    }

    // Update is called once per frame
    void Update()
    {
        if(isCrossing)
        {
            PlatformEffector.rotationalOffset = 180f;
            isCrossing = false;
        }
    }
}
