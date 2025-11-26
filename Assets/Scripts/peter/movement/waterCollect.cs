using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class waterCollect : MonoBehaviour
{
    playerStateManager PlayerState;

    GameObject peterHead;

    GameObject childWaterdrop;

    public bool touchPed = false;

    baseTimer Timer;

    void Start()
    {
        assignObjects();
        Timer=GetComponent<baseTimer>();
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

       if(Timer.isEnd())
        {
            destroyChildWater();
            Timer.ResetTimer();
        }
    }

    //when head touches the ground, drop the seed
    private void DropChildWater()
    {
        if (childWaterdrop != null)
        {
            childWaterdrop.transform.position = peterHead.transform.position + new Vector3(8f, 4f, 0f);
            childWaterdrop.transform.rotation = Quaternion.identity;
            childWaterdrop.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            childWaterdrop.GetComponent<Rigidbody2D>().simulated = true;
            childWaterdrop.GetComponent<Rigidbody2D>().AddForce(new Vector2(1f,1f)*4f,ForceMode2D.Impulse);

            //turn on simulation
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

    //logic for when the player collects the seed
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //pick up seed
        if (collision.gameObject.CompareTag("WaterDrop"))
        {
            PlayerState.isHeavy = true;

            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            collision.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            collision.gameObject.transform.position = peterHead.transform.position;
            collision.gameObject.transform.SetParent(peterHead.transform);
            
            
            childWaterdrop = collision.gameObject;

            Timer.StartTimer();
        }
        //when it touches the pedestal
        else if(collision.gameObject.CompareTag("Pedestal"))
        {
            PlayerState.isHeavy = false;

            touchPed = true;

            Timer.ResetTimer();
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
