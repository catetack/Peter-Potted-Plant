using System.Security.Cryptography;
using UnityEngine;

public class headRotations : MonoBehaviour
{
    playerStateManager PlayerState;
    float rotationClamp;
    float rotationSpeed;
    float torqueModifier;
    float lightTorque;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AssignObjects();
    }

    void AssignObjects()
    {
        torqueModifier = 300.0f;
        lightTorque = 5.0f;
        PlayerState = GetComponentInParent<playerStateManager>();
        if (PlayerState == null) Debug.LogWarning("playerStateManager script not found on 'PlayerState' parent.");
    }
    // Update is called once per frame
    void Update()
    {

        if (!PlayerState.isDowned)
        {
            playerRotations();
            LightRotation();
        }
        if (PlayerState.isDowned)
        {
            rotationSpeed = 0.0f;
        }
        transform.Rotate(0, 0, rotationSpeed);

    }

    void playerRotations()
    {
        NormalizeRotation();
        rotationSpeed = PlayerState.headTorque * torqueModifier * Time.deltaTime;
    }
    void NormalizeRotation()
    {
        
        rotationClamp = (transform.eulerAngles.z > 180f) ? transform.eulerAngles.z - 360f : transform.eulerAngles.z;
    }
    void LightRotation()
    {

        rotationSpeed -= rotationClamp * lightTorque * Time.deltaTime;
        Debug.Log("Light rotation applied: " + (rotationSpeed));
    }
}
