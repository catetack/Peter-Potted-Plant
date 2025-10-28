using System;
using UnityEngine;

public class wheelSpin : MonoBehaviour
{
    playerStateManager PlayerState;
    Vector3 spriteSize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        assignObjects();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, -PlayerState.displacementSpeed * (2.0f*Mathf.PI*(spriteSize.y)));
    }
    
    void assignObjects()
    {
        PlayerState = GetComponentInParent<playerStateManager>();
        if (PlayerState == null)
        {
            Debug.LogError("playerStateManager script not found on 'PlayerState' parent.");
        }
        spriteSize = GetComponent<Renderer>().bounds.size;
        Debug.Log("Wheel Size - Width: " + spriteSize.x + " Height: " + spriteSize.y + " Depth: " + spriteSize.z);
    }
}
