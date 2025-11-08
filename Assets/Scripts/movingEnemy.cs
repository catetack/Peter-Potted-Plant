using UnityEngine;
using UnityEngine.UIElements;


public enum EnemyState
{
    Patrol,Chase
}
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
    public EnemyState currentState;
    public Transform playerTf;
    public Transform spawnTf;
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
        currentState = EnemyState.Patrol;
    }

    // Update is called once per frame
    void Update()
    {
        playerTf = GameObject.Find("Displacement").GetComponent<Transform>();

        if (playerTf != null)
        {
            //First, make sure the enemy chase only when the player is in the range
            awayfromCenter = (transform.position - spawnTf.position).sqrMagnitude;
            if(awayfromCenter < rangeRadius )
            {
                distance = (transform.position - playerTf.position).sqrMagnitude;
                if (distance < chasingRadius)
                {
                    currentState = EnemyState.Chase;
                }
                
            }
        }

        switch (currentState)
        {
            case EnemyState.Patrol:
                regularMove();
                break;
            case EnemyState.Chase:
                Chasing();
                break;
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
        awayfromCenter = (transform.position - spawnTf.position).sqrMagnitude;
        if (awayfromCenter > rangeRadius)
        {
            currentState = EnemyState.Patrol;
        }
        transform.position = Vector2.MoveTowards(transform.position,playerTf.position,chasingSpeed*Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
       Gizmos.color = Color.red;
       Gizmos.DrawWireSphere(transform.position, chasingRadius);

       Gizmos.color = Color.yellow;
       Gizmos.DrawWireSphere(spawnTf.position, rangeRadius);
    }

    private void MoveCheck()//Check the movement to flip,for animation
    {

    }
}
