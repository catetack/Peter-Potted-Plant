using UnityEngine;
using UnityEngine.Audio;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb;

    [Range(1,10)]
    public float jumpSpeed = 5.0f;

    private bool jumpPressed;

    public bool isGrounded;

    public Transform groundCheck;

    public LayerMask ground;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //Jump
        jumpInput();
        jump();
    }

    private void FixedUpdate()//Physics
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,0.1f,ground);
    }

    private void jumpInput()
    {
        jumpPressed = Input.GetButtonDown("Jump");
    }

    private void jump() 
    {
        if (jumpPressed&&isGrounded)
        {
            rb.linearVelocity=Vector2.up*jumpSpeed;
        }
    }
}
