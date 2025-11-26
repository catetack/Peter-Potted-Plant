using UnityEngine;

public class RotateTiltMeter : MonoBehaviour
{
    playerStateManager PlayerState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        assignObjects();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, -30.0f + 60.0f * PlayerState.rotationRatio);
        Debug.Log(PlayerState.rotationRatio);
    }

    void assignObjects()
    {
        PlayerState = GetComponentInParent<playerStateManager>();
        if (PlayerState == null)
        {
            Debug.LogError("playerStateManager script not found on 'PlayerState' parent.");
        }
    }
}
