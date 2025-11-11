using UnityEngine;

public class waterDropManager : MonoBehaviour
{

    playerStateManager pState;
    bool playerLastDeathState = false;
    public bool isCollected=false;
    public GameObject childWaterDrop;
    public GameObject waterDropBase;

    waterCollect playerWaterCollect;

    private void Start()
    {
        pState = GameObject.Find("Player").GetComponent<playerStateManager>();
        playerWaterCollect = GameObject.Find("Displacement").GetComponent<waterCollect>();

    }
    private void Update()
    {
        if (((!playerLastDeathState && pState.isDowned) || playerWaterCollect.touchPed)&&isCollected)
        //if (!playerLastDeathState && pState.isDowned && isCollected)
        {
            isCollected = false;
            GameObject newDrop = Instantiate(waterDropBase, transform.position, Quaternion.identity, transform);
            childWaterDrop = newDrop;

            playerWaterCollect.touchPed = false;
        }

        playerLastDeathState=pState.isDowned;

        if(childWaterDrop == null)
        {
            isCollected = true;
        }
    }
}
