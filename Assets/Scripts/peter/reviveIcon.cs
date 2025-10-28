using UnityEngine;

public class reviveIcon : MonoBehaviour
{
    playerStateManager PlayerState;

    float offset = 5.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        assignObjects();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerState.isDowned)
        {
            GetComponent<Renderer>().enabled = false;
        }
        else
        {
            GetComponent<Renderer>().enabled = true;
        }
    }
    void LateUpdate()
    {
        transform.position = new Vector3(PlayerState.headPosition.x + offset, PlayerState.headPosition.y + offset, transform.position.z);
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
