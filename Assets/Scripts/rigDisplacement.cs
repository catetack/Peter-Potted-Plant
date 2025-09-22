using UnityEngine;

public class rigDisplacement : MonoBehaviour
{
    public GameObject player;

    InputSystem_Actions inputActions;

    Vector2 controllerInput;
    float legsThrottle;
    float speedModifier = 0.1f;
    float speed;

    Transform childTransform;
    float rigRot;
    float rotationClamp;
    public float tiltToSpeedRatio = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();

        childTransform = transform.Find("Rotation");
        if (childTransform != null)
        {
            rigRot = childTransform.eulerAngles.z;
            if (rigRot != null)
            {
                //thatZRotation = rigRot;
                Debug.Log("Initial Z Rotation from rigRotation: " + rigRot);
            }
            else
            {
                Debug.LogError("rigRotation component not found on the 'Rotation' child GameObject.");
            }
        }
        else
        {
            Debug.LogError("'Rotation' child GameObject not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //rig rotation function:
        rigRot = childTransform.eulerAngles.z;
        rotationClamp = (rigRot > 180f) ? rigRot - 360f : rigRot;
        tiltToSpeedRatio = -rotationClamp * 0.1f; // normalize the tilt to a range of -1 to 1 based on max tilt angle of 90 degrees


        controllerInput = inputActions.Player.Legs.ReadValue<Vector2>();
        legsThrottle = controllerInput.x;
       
        speed = (legsThrottle + tiltToSpeedRatio) * speedModifier;
        player.transform.Translate(speed, 0, 0);
    }
}
