using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private InputSystem_Actions  inputAction;
    Rigidbody2D rb;

    [Range(1,10)]
    public float jumpSpeed = 5.0f;

    private bool jumpPressed;

    private bool jumpHold;

    public bool isGrounded;

    public Transform groundCheck;

    public LayerMask ground;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        inputAction = new InputSystem_Actions();
        inputAction.Enable();

    }

    // Update is called once per frame
    void Update()
    {
        //Jump
        jumpInput();
    }

    private void FixedUpdate()//Physics
    {
        onGround();
        jump();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

    private void jumpInput()
    {
        jumpPressed = inputAction.Player.Jump.IsPressed();
    }

    private void jump() 
    {
        if (jumpPressed&&isGrounded)
        {
            rb.linearVelocity=Vector2.up*jumpSpeed;
        }
    }

    private void onGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
    }
}
