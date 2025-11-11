using System;
using UnityEngine;

public class headCollisionHandler : MonoBehaviour
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
        
    }
    void assignObjects()
    {
        PlayerState = GetComponentInParent<playerStateManager>();
        if (PlayerState == null)
        {
            Debug.LogError("playerStateManager script not found on 'PlayerState' parent.");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.name == "Ground")
        {
            PlayerState.isDowned = true;
            PlayerState.isHeavy = false;
            PlayerState.health = 0.0f;
        }
    }

}
