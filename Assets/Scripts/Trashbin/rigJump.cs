using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class rigJump : MonoBehaviour
{
    private InputSystem_Actions  inputAction;
    Rigidbody2D playerRigidbody;

    //Jump
    //[Range(4,10)]//sliders for jumpMinSpeed
    //public float jumpMinSpeed = 8.0f;

    [Range(10,24)]//sliders for jumpMinSpeed
    public float jumpSpeed = 16.0f;

    //[Range(11, 20)]//sliders for jumpMaxSpeed
    //public float jumpMaxSpeed = 12.0f;

    private bool jumpStart=false;

    //private float chargeStart=0f;

    [Range(1, 10)]//sliders for faster falling
    public float fallAddition = 3.5f;

    [Range(1, 5)]//sliders for max falling speed
    public float maxFallSpeed = 100f;

    //Ground check
    public bool isGrounded;

    public Transform groundCheck;

    public LayerMask ground;

    //for initializing Sound Effects
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputAction = new InputSystem_Actions();

        inputAction.Enable();
        //inputAction.Player.Jump.started += ctx => chargeStart = Time.time;
        //inputAction.Player.Jump.performed += ctx => jumpStart = true;
        //inputAction.Player.Jump.canceled += ctx => jumpStart = true;

        playerRigidbody =GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Jump
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
    private void Jump() 
    {
        if(playerRigidbody.linearVelocityY < 0f)
        {
            if(playerRigidbody.linearVelocityY< maxFallSpeed)
            {
                playerRigidbody.linearVelocityY += Physics2D.gravity.y * (fallAddition - 1) * Time.fixedDeltaTime;//Add an acceleration when falling
            }
            else
            {
                playerRigidbody.linearVelocityY = maxFallSpeed;
            }
        }

        if (isGrounded && jumpStart)
        {
            //float chargeTime = Time.time - chargeStart;
            //float jumpForce = Mathf.Clamp(chargeTime * 100f, jumpMinSpeed, jumpMaxSpeed);

            audioManager.PlaySoundEffect(audioManager.jump);
            playerRigidbody.linearVelocity = Vector2.up * jumpSpeed;
            //chargeStart = 0f;
            jumpStart = false;
        }
    }

    private void onGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
    }
}
