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
    }
    // Update is called once per frame
    void Update()
    {
        if(isBursting)
        {
            return;
        }

        Burst();
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
        if(Input.GetKeyDown(KeyCode.Space))//Here is the input, which could change later
        {
            Ani.Play("Burst");
            isBursting = true;
            Invoke("BurstEnd",0.3333f);
        }
    }

    public void BurstEnd()
    {
        isBursting=false;
    }

    public void CheckBurstBox()
    {
        //Draw the Gizmo box frame
        float width = 4;
        float height=4;
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
            //Shake the camera
            cameraShake.Instance.shakeStart(0.06f,0.2f);

            Debug.Log(col.transform.name);
            Destroy(col.gameObject);
        }
    }
}
