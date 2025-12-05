using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class groundTrigger : MonoBehaviour
{
    public GameObject targetGround;
    CompositeCollider2D targetTilemapCollider;

    public bool disableGround=false;

    SpriteRenderer SR;

    private void Start()
    {
        SR= GetComponent<SpriteRenderer>();
        targetTilemapCollider= targetGround.GetComponent<CompositeCollider2D>();
        Debug.Log("Enabled ground collider" + targetTilemapCollider.name);
        //targetTilemapCollider.enabled = true;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="PlayerHillBlock")
        {
            if (targetGround!=null) 
            {
                if(!disableGround)
                {
                    targetTilemapCollider.isTrigger = false;
                    targetGround.gameObject.tag = "Ground";
                    Debug.Log("Disabled ground collider" + targetTilemapCollider.name);
                }
                else
                {
                    targetTilemapCollider.isTrigger = true;
                    targetGround.gameObject.tag = "Untagged";
                    Debug.Log("Enabled ground collider" + targetTilemapCollider.name);
                }
            }
        }
    }
}
