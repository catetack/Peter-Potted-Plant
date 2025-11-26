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
    Transform playerTf;
    Transform patrolCenterTf;
    playerStateManager pState;
    float distancePtf;
    float awayfromCenter;
    public float chasingRadius =10f;
    public float chasingSpeed = 5f;
    public float rangeRadius = 80f;

    //Animation
    Animator Ani;
    SpriteRenderer spriteRender;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = startWaitTime;
        //spawnPos=GetComponent<Transform>();
        currentState = EnemyState.Patrol;

        //playerTf = GameObject.Find("Displacement").GetComponent<Transform>();
        playerTf = GameObject.Find("peterHead").GetComponent<Transform>();
        patrolCenterTf = transform.parent.Find("PatrolCenter");

        pState=GameObject.Find("Player").GetComponent<playerStateManager>();

        Ani = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Chasing logic
        if (playerTf != null)
        {
            //First, make sure the enemy chase only when the player is in the range
            awayfromCenter = (playerTf.position - patrolCenterTf.position).sqrMagnitude;
            distancePtf = (transform.position - playerTf.position).sqrMagnitude;
            if ((awayfromCenter < rangeRadius* rangeRadius && distancePtf < chasingRadius * chasingRadius)&&!pState.isDowned)
            {
                currentState = EnemyState.Chase;
                gameObject.tag = "Ground";
            }
            else
            {
                currentState = EnemyState.Patrol;
                gameObject.tag = "Untaggeed";
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

        //Sprite flip
        MoveCheck();
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
            //Wait for seconds
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    void Chasing()
    {
        transform.position = Vector2.MoveTowards(transform.position,playerTf.position,chasingSpeed*Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
       Gizmos.color = Color.red;
       Gizmos.DrawWireSphere(transform.position, chasingRadius);

       Gizmos.color = Color.yellow;
       Gizmos.DrawWireSphere(patrolCenterTf.position, rangeRadius);
    }

    private void MoveCheck()//Check the movement to flip,for animation
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                if (transform.position.x > partrolPoints[i].transform.position.x)
                {
                    spriteRender.flipX = false;
                }
                else if (transform.position.x == partrolPoints[i].transform.position.x)
                {

                }
                else
                {
                    spriteRender.flipX = true;
                }
                break;
            case EnemyState.Chase:
                if (transform.position.x > playerTf.position.x)
                {
                    spriteRender.flipX = false;
                }
                else if (transform.position.x == playerTf.position.x)
                {

                }
                else
                {
                    spriteRender.flipX = true;
                }
                break;
        }

        
    }
}
