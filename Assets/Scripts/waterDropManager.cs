using UnityEngine;

public class waterDropManager : MonoBehaviour
{

    playerStateManager pState;
    bool playerLastDeathState = false;
    public bool isCollected=false;
    public GameObject childWaterDrop;
    public GameObject waterDropBase;

    // coin
    public GameObject coinPrefab;
    public Transform playerHead;
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

    }

    public void OnDropCollected()
    {
        // spawn coin when water drop collected
        if (coinPrefab != null && playerHead != null)
        {
            GameObject coin = Instantiate(coinPrefab, playerHead.position, Quaternion.identity, playerHead);
            coin.transform.localPosition = new Vector3(0f, 0.5f, 0f);
        }

        isCollected = true;
    }

}
