using UnityEngine;
using UnityEngine.UI;

public class FallingCheck : MonoBehaviour
{

    bool isGrounded;
    bool wasGrounded;
    public LayerMask ground;
    public Transform groundCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGrounded = false;
        wasGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        onGround();
        
    }
    
    private void onGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        if (isGrounded != wasGrounded)
        {
            
            wasGrounded = isGrounded;
            Debug.Log("isGrounded: " + isGrounded);
        }
        
    }
}
