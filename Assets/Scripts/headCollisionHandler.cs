using System;
using UnityEngine;

public class headCollisionHandler : MonoBehaviour
{
    bool isColliding = false;
    playerStateManager PlayerState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        assignObjects();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void assignObjects()
    {
        PlayerState = GetComponentInParent<playerStateManager>();
        if (PlayerState == null)
        {
            Debug.LogError("playerStateManager script not found on 'PlayerState' parent.");
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
        // Add specific actions here, e.g., applying damage, playing a sound
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.name == "Ground")
        {
            isColliding = true;
            PlayerState.isDowned = true;
        }
        Debug.Log("Triggered by: " + other.gameObject.name);
        Debug.Log("Is Colliding: " + isColliding);
        // Add specific actions for trigger events
    }

}
