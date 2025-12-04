using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class waterDropManager : MonoBehaviour
{

    playerStateManager pState;
    //bool playerLastDeathState = false;
    public bool hasChild=true;
    public GameObject waterDropBase;
    public GameObject childWaterDrop;

    private Transform playerHead;
    waterCollect playerWaterCollect;

    private baseTimer Timer;

    public bool i;

    //Timer UI
    private GameObject TimerUI;

    float r;

    private int lastIndex = -1;

    private void Start()
    {
        pState = GameObject.Find("Player").GetComponent<playerStateManager>();
        playerHead = GameObject.Find("peterHead").transform;
        playerWaterCollect = GameObject.Find("Displacement").GetComponent<waterCollect>();
        childWaterDrop = transform.Find("WaterDropBase").gameObject;
        Timer= GetComponent<baseTimer>();
        TimerUI = GameObject.Find("TimerUI");
    }

    private void Update()
    {
        i = playerWaterCollect.hasWaterDrop();
        checkCollected();
        generateNew();

        if(Timer.currentTime<Timer.duration)
        {
            TimerUIAni();
        }
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
                Color c= TimerUI.GetComponent<Image>().color;
                c.a = 1.0f;
                TimerUI.GetComponent<Image>().color = c;
                TimerUI.GetComponent <Animator>().speed = 1.0f;
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

        pState.isHeavy = false;

        playerWaterCollect.destroyChildWater();

        childWaterDrop = newDrop;
        
        Timer.ResetTimer();

        TimerUI.GetComponent<Animator>().Play("Timer1");

        Color c = TimerUI.GetComponent<Image>().color;
        c.a = 0f;
        TimerUI.GetComponent<Image>().color = c;

        //playerWaterCollect.touchPed = false;
    }

    private int GetTimerIndex(float r)
    {
        if (r > 0.875f) return 1;
        else if (r > 0.75f) return 2;
        else if (r > 0.625f) return 3;
        else if (r > 0.5f) return 4;
        else if (r > 0.375f) return 5;
        else if (r > 0.25f) return 6;
        else if (r > 0.125f) return 7;
        else return 8;
    }

    private void TimerUIAni()
    {
        r = Timer.currentTime / Timer.duration;
        if(r>1||r<0)
        {
            return;
        }

        int index = GetTimerIndex(r);

        // play the animation then index is changed
        if (index != lastIndex)
        {
            lastIndex = index;
            TimerUI.GetComponent<Animator>().speed = 1f + (index - 1) / 7;
            TimerUI.GetComponent<Animator>().Play("Timer" + index);
        }
    }
}
