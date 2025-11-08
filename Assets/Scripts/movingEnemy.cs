using UnityEngine;
using UnityEngine.UIElements;

public class movingEnemy : MonoBehaviour
{
    //[SerializeField]

    //Regular movement
    private float timer;
    public float startWaitTime = 2f;
    public Transform[] partrolPoints;
    public float normalSpeed = 3f;
    int i = 0;

    //Locate player and chasing
    public bool playerisFound;
    public Transform playerPos;
    public Transform spawnPos;
    float distance;
    float awayfromCenter;
    public float chasingRadius =10f;
    public float chasingSpeed = 5f;
    public float rangeRadius = 80f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = startWaitTime;
        //spawnPos=GetComponent<Transform>();
        playerisFound = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = GameObject.Find("Displacement").GetComponent<Transform>();

        if (playerPos != null)
        {
            distance = (transform.position - playerPos.position).sqrMagnitude;
            if (distance < chasingRadius)
            {
                playerisFound = true;
            }
            else
            {
                playerisFound= false;
            }
        }

        if (playerisFound )
        {
            Chasing();
        }
        else
        {
            regularMove();
        }

    }

    void regularMove()
    {
        transform.position = Vector2.MoveTowards(transform.position, partrolPoints[i].transform.position, normalSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, partrolPoints[i].transform.position) < 0.1f)
        {
            if (timer <= 0)
            {
                //Switch the points
                if (partrolPoints[i] != partrolPoints[partrolPoints.Length - 1])
                {
                    i++;
                }

                else
                {
                    i = 0;
                }
                timer = startWaitTime;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    void Chasing()
    {
        transform.position = Vector2.MoveTowards(transform.position,playerPos.position,chasingSpeed*Time.deltaTime);
        //awayfromCenter = (transform.position - spawnPos.position).sqrMagnitude;
        //if (awayfromCenter > rangeRadius)
        //{
            //playerisFound = false;
        //}

    }

    private void OnDrawGizmos()
    {
       Gizmos.color = Color.red;
       Gizmos.DrawWireSphere(transform.position, chasingRadius);

       Gizmos.color = Color.yellow;
       Gizmos.DrawWireSphere(spawnPos.position, rangeRadius);
    }

    private void MoveCheck()//Check the movement to flip,for animation
    {

    }
}
