using UnityEngine;

public class followPlayer : MonoBehaviour
{
    GameObject player;
    Vector3 playerPosition;

    public float verticalOffset = 5.0f; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("peterFeet");
        if (player == null)
        {
            Debug.LogError("Player GameObject not found. Please ensure there is a GameObject named 'peterFeet' in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        transform.position = new Vector3(playerPosition.x, playerPosition.y + verticalOffset, transform.position.z);
    }
}
