using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class waterCollect : MonoBehaviour
{
    playerStateManager PlayerState;
    public waterDropManager dropManager;

    private void Start()
    {
        assignObjects();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WaterDrop"))
        {
            PlayerState.isHeavy = true;
            dropManager.OnDropCollected();
            Destroy(collision.gameObject);
        }
    }

    void assignObjects()
    {
        PlayerState = GetComponentInParent<playerStateManager>();
        dropManager = FindObjectOfType<waterDropManager>();
    }
}
