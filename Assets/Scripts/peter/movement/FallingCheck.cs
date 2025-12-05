using UnityEngine;
using UnityEngine.UI;

public class FallingCheck : MonoBehaviour
{
    //checks when player is on the ground or not. Used in rotation script to make controlling the character easier when in the air.
    playerStateManager PlayerState;
    public bool Is_Grounded;
    bool wasGrounded;
    public LayerMask ground;
    public Transform groundCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AssignObjects();
        Is_Grounded = false;
        PlayerState.isGrounded = false;
    }

    void AssignObjects()
    {
        PlayerState = GameObject.FindWithTag("Player").GetComponent<playerStateManager>();
    }
    // Update is called once per frame
    void Update()
    {

        onGround();
    }
    
    private void onGround()
    {
        Is_Grounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        if (Is_Grounded != PlayerState.isGrounded)
        {   
            PlayerState.isGrounded = Is_Grounded;
        }
        
    }
}
