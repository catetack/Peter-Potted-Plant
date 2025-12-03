using UnityEngine;

public class bloomBurst : MonoBehaviour
{
    Animator Ani;
    bool isBursting = false;

    InputSystem_Actions inputActions;
    void Start()
    {
        Ani=GetComponent<Animator>();
    }
    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Burst.started += ctx => Burst();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }
    // Update is called once per frame
    void Update()
    {
        if(isBursting)
        {
            return;
        }

        BurstwithSpace();
    }

    private void LateUpdate()
    {
        if(isBursting)
        {
            return;
        }

        if (Ani != null)
        {
            Ani.Play("BurstIdle");
        }
    }

    public void Burst()
    {
            Ani.Play("Burst");
            isBursting = true;
            Invoke("BurstEnd",0.3333f);
    }

    public void BurstwithSpace()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Ani.Play("Burst");
            isBursting = true;
            Invoke("BurstEnd", 0.3333f);
        }
    }

    public void BurstEnd()
    {
        isBursting=false;
    }

    public void CheckBurstBox()
    {
        //Draw the Gizmo box frame
        float width = 6;
        float height=6;
        Vector2 pos1 = transform.position+ transform.right*width*0.5f+transform.up*height * 0.5f;
        Vector2 pos2 = transform.position + transform.right * width * 0.5f - transform.up * height * 0.5f;
        Vector2 pos3 = transform.position - transform.right * width * 0.5f + transform.up * height * 0.5f;
        Vector2 pos4 = transform.position - transform.right * width * 0.5f - transform.up * height * 0.5f;

        Debug.DrawLine(pos1,pos2,Color.green,0.25f);
        Debug.DrawLine(pos4, pos2, Color.green, 0.25f);
        Debug.DrawLine(pos3, pos4, Color.green, 0.25f);
        Debug.DrawLine(pos1, pos3, Color.green, 0.25f);

        //Collider check
        Collider2D col= Physics2D.OverlapBox(transform.position,new Vector2(width,height),0,LayerMask.GetMask("Enemy"));
        if(col!=null)
        {
            //Debug.Log("Collision called");
            Destroy(col.gameObject);
            //Shake the camera
            cameraShake.Instance.shakeStart(0.06f,0.2f);

            Debug.Log(col.transform.name);
        }
    }
}
