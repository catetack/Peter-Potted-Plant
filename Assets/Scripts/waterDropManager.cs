using UnityEngine;

public class waterDropManager : MonoBehaviour
{

    playerStateManager pState;
    bool playerLastDeathState = false;
    public bool isCollected=false;
    public GameObject childWaterDrop;
    public GameObject waterDropBase;

    private void Start()
    {
        pState = GameObject.Find("Player").GetComponent<playerStateManager>();
    }
    private void Update()
    {
        if(!playerLastDeathState&&pState.isDowned&&isCollected)
        {
            isCollected = false;
            GameObject newDrop = Instantiate(waterDropBase, transform.position, Quaternion.identity, transform);
            childWaterDrop = newDrop;
        }

        playerLastDeathState=pState.isDowned;

        if(childWaterDrop == null)
        {
            isCollected = true;
        }
    }
}
