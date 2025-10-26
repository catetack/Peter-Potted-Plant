using UnityEngine;

public class headCollisionHandler : MonoBehaviour
{
    public bool isColliding = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
        Debug.Log("Triggered by: " + other.gameObject.name);
        Debug.Log("Is Colliding: " + isColliding);
        // Add specific actions for trigger events
    }

}
