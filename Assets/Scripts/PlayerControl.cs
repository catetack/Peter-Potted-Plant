using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private InputSystem_Actions  inputAction;
    Rigidbody2D rb;

    //Jump
    [Range(4,10)]
    public float jumpMinSpeed = 8.0f;

    [Range(11, 20)]
    public float jumpMaxSpeed = 12.0f;

    private bool jumpStart=false;

    private float chargeStart=0f;

    //Better Jump
    public float fallAddition = 3.5f;
    public float jumpAddition = 3.5f;

    //Ground check
    public bool isGrounded;

    public Transform groundCheck;

    public LayerMask ground;

    private void Awake()
    {
        inputAction = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputAction.Enable();
        inputAction.Player.Jump.started += ctx => chargeStart = Time.time;
        inputAction.Player.Jump.canceled += ctx => jumpStart = true;
    }

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
    }

    private void FixedUpdate()//Physics
    {
        onGround();
        Jump();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

    private void jumpInput()
    {
        //jumpPressed = inputAction.Player.Jump.IsPressed();
        //Debug.Log(jumpPressed);
    }

    private void Jump() 
    {
        if(rb.linearVelocityY < 0f)
        {

        }

        if (isGrounded&&jumpStart)
        {
            float chargeTime = Time.time - chargeStart;
            Debug.Log(chargeTime*100f);
            float jumpForce = Mathf.Clamp(chargeTime * 100f, jumpMinSpeed, jumpMaxSpeed);
            rb.linearVelocity=Vector2.up* jumpForce;
            chargeStart = 0f;
            jumpStart = false;
        }
    }

    private void onGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
    }
}
