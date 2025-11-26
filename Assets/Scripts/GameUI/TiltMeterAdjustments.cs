using Unity.VisualScripting;
using UnityEngine;

public class TiltMeterAdjustments : MonoBehaviour
{   
    float ROTATION_SCALE = 100.0f;
    float rotationScale;
    float rotationAmount;
    playerStateManager PlayerState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        assignObjects();
    }

    // Update is called once per frame
    void Update()
    {
        rotationAmount = -rotationScale * PlayerState.rotationRatio;
        if (rotationAmount < 0)
        {
            rotationAmount = rotationAmount + rotationScale;
        }
        else    
        {
            rotationAmount = rotationAmount - rotationScale;
        }

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationAmount);
    }

    void assignObjects()
    {
        rotationScale = ROTATION_SCALE;
        PlayerState = GetComponentInParent<playerStateManager>();
        if (PlayerState == null)
        {
            Debug.LogError("playerStateManager script not found on 'PlayerState' parent.");
        }
    }
}
