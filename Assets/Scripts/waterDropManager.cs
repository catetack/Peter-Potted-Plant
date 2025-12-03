using System.Threading;
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

    private baseTimer Timer;

    public bool i;

    private void Start()
    {
        pState = GameObject.Find("Player").GetComponent<playerStateManager>();
        playerHead = GameObject.Find("peterHead").transform;
        playerWaterCollect = GameObject.Find("Displacement").GetComponent<waterCollect>();
        childWaterDrop = transform.Find("WaterDropBase").gameObject;
        Timer= GetComponent<baseTimer>();
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
            if(!Timer.isRunning)
            {
                Timer.StartTimer();
            }
            
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
        if(!hasChild)
        {
            if (playerWaterCollect.touchPed && playerWaterCollect.hasWaterDrop())
            {
                SpawnNewDrop();
            }

            else if(Timer.isEnd)
            {
                SpawnNewDrop();
                Timer.isEnd = false;
            }
        }
        

        //playerLastDeathState = pState.isDowned;
    }

    public void SpawnNewDrop()
    {
        //hasChild = true;

        GameObject newDrop = Instantiate(waterDropBase, transform.position, Quaternion.identity, transform);
        //GameObject newDrop = Instantiate(waterDropBase, transform.position, Quaternion.identity, newPos);

        childWaterDrop = newDrop;

        if(playerWaterCollect.hasWaterDrop())
        {
            playerWaterCollect.destroyChildWater();

            pState.isHeavy = false;
        }
        
        Timer.ResetTimer();

        //playerWaterCollect.touchPed = false;
    }
}
