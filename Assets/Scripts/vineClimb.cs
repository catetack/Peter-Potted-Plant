using UnityEngine;

public class vineClimb : MonoBehaviour
{
    private float vertical;
    private float speed = 8f;
    private bool isLadder;
    private bool isClimbing;

    public Animator peterAnimator;

    [SerializeField] private Rigidbody2D rb;

    float originGScale;

    private void Start()
    {
        if(rb != null)
        {
            originGScale = rb.gravityScale;
        }
        
    }

    void Update()
    {
        peterAnimator = GetComponentInChildren<Animator>();
        vertical = Input.GetAxisRaw("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * speed);
        }
        else
        {
            rb.gravityScale = originGScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
            peterAnimator.SetBool("Climbing", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            peterAnimator.SetBool("Climbing", false);
            isLadder = false;
            isClimbing = false;
        }
    }
}
