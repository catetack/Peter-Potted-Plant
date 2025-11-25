using UnityEngine;

public class groundTrigger : MonoBehaviour
{
    public GameObject targetGround;

    public bool disableGround=false;

    SpriteRenderer SR;

    private void Start()
    {
        SR= GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            if(targetGround!=null) 
            {
                if(!disableGround)
                {
                    targetGround.gameObject.tag = "Ground";
                }
                else
                {
                    targetGround.gameObject.tag = "Untagged";

                }
            }
        }
    }
}
