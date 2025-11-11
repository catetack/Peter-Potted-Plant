using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class waterCollect : MonoBehaviour
{
    playerStateManager PlayerState;

    public bool touchPed = false;

    void Start()
    {
        assignObjects();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("WaterDrop"))
        {
            PlayerState.isHeavy = true;
            Destroy(collision.gameObject);
        }

        else if(collision.gameObject.CompareTag("Pedestal"))
        {
            PlayerState.isHeavy = false;
            touchPed = true;
        }
    }

    void assignObjects()
    {
        PlayerState = GetComponentInParent<playerStateManager>();
        if (PlayerState == null)
        {
            Debug.LogError("playerStateManager script not found on 'PlayerState' parent.");
        }
    }
}
