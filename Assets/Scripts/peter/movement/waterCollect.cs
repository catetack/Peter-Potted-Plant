using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class waterCollect : MonoBehaviour
{
    playerStateManager PlayerState;

    GameObject peterHead;

    GameObject childWaterdrop;

    //private void Start();
    public bool touchPed = false;

    void Start()
    {
        assignObjects();
    }

    private void Update()
    {
       if(!touchPed)//Drop the waterdrop
        {
            if(PlayerState.isHeavy == false)
            {
                DropChildWater();
            }
        }
    }

    private void DropChildWater()
    {
        if (childWaterdrop != null)
        {
            childWaterdrop.transform.position = peterHead.transform.position + new Vector3(8f, 4f, 0f);
            childWaterdrop.transform.rotation = Quaternion.identity;
            childWaterdrop.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            childWaterdrop.GetComponent<Rigidbody2D>().AddForce(new Vector2(1f,1f)*4f,ForceMode2D.Impulse);


            childWaterdrop.transform.SetParent(null);

            childWaterdrop = null;
        }
    }

    public bool hasWaterDrop()
    {
        if(childWaterdrop != null)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public void destroyChildWater()
    {
        if (childWaterdrop != null)
        {
            Destroy(childWaterdrop);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WaterDrop"))
        {
            PlayerState.isHeavy = true;

            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            collision.gameObject.transform.position = peterHead.transform.position;
            collision.gameObject.transform.SetParent(peterHead.transform);
            
            
            childWaterdrop = collision.gameObject;
        }

        else if(collision.gameObject.CompareTag("Pedestal"))
        {
            PlayerState.isHeavy = false;

            touchPed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pedestal"))
        {
            touchPed = false;
        }
    }

    void assignObjects()
    {
        PlayerState = GetComponentInParent<playerStateManager>();
        peterHead = GameObject.Find("peterHead");
    }
}
