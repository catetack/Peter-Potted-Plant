using UnityEngine;

public class armRotation : MonoBehaviour
{
    playerStateManager PlayerState;
    const float BASE_MOVEMENT_TORQUE = 600.0f;
    float resultingMovementTorque;
    float targetRotationSpeed;
    float lightTorque;

    float armRotationValue = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerState = GameObject.FindWithTag("Player").GetComponent<playerStateManager>();
        lightTorque = 5.0f;
        resultingMovementTorque = 0.0f;
        targetRotationSpeed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerState.isDowned)
        {
            movementRotations();
            lightRotation();
            
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, targetRotationSpeed);
        }
        //armRotationValue is affected by speed 
    }

    void movementRotations()
    {
        //reduce effect of movement on rotation when closer to the bottom
        resultingMovementTorque = BASE_MOVEMENT_TORQUE * (PlayerState.rotationRatio*PlayerState.rotationRatio);
        targetRotationSpeed = PlayerState.displacementSpeed * resultingMovementTorque * Time.deltaTime;
    }

    void lightRotation()
    {
        //apply a small torque to bring peter back to upright when close to upright
        if (Mathf.Abs(PlayerState.rotationRatio) < 0.2f)
        {
            lightTorque = -PlayerState.rotationRatio * 200.0f * Time.deltaTime;
            targetRotationSpeed = targetRotationSpeed + lightTorque;
        }
    }

}
