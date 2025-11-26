using UnityEngine;

public class waterDropManager : MonoBehaviour
{

    playerStateManager pState;
    //bool playerLastDeathState = false;
    public bool hasChild=false;
    public GameObject waterDropBase;
    public GameObject childWaterDrop;

    private Transform playerHead;
    waterCollect playerWaterCollect;

    public bool i;

    //Timer
    baseTimer Timer;

    private void Start()
    {
        pState = GameObject.Find("Player").GetComponent<playerStateManager>();
        playerHead = GameObject.Find("peterHead").transform;
        playerWaterCollect = GameObject.Find("Displacement").GetComponent<waterCollect>();
        childWaterDrop = transform.Find("WaterDropBase").gameObject;
        Timer=GetComponent<baseTimer>();
    }

    private void Update()
    {
        i = playerWaterCollect.hasWaterDrop();
        checkCollected();
        generateNew();
    }

    //checks when gets collected by player
    private void checkCollected()
    {
        if(transform.childCount<=0)
        {
            hasChild = false;
        }
        else
        {
            hasChild = true;
        }
    }

    //respawning function
    private void generateNew()
    {
        //if (((!playerLastDeathState && pState.isDowned) || playerWaterCollect.touchPed) && isCollected)
        if (playerWaterCollect.touchPed && !hasChild&&playerWaterCollect.hasWaterDrop()||Timer.isEnd())
        {
            //hasChild = true;

            GameObject newDrop = Instantiate(waterDropBase, transform.position, Quaternion.identity, transform);
            //GameObject newDrop = Instantiate(waterDropBase, transform.position, Quaternion.identity, newPos);

            childWaterDrop = newDrop;

            playerWaterCollect.destroyChildWater();

            Timer.ResetTimer();

            //playerWaterCollect.touchPed = false;
        }

        //playerLastDeathState = pState.isDowned;
    }
}
