using UnityEngine;

public class movingEnemy : MonoBehaviour
{
    //[SerializeField]

    private float timer;
    public float startWaitTime = 2f;
    public Transform[] partrolPoints;
    public float speed = 3f;
    int i = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = startWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, partrolPoints[i].transform.position,speed*Time.deltaTime);

        if(Vector2.Distance(transform.position, partrolPoints[i].transform.position)<0.1f)
        {
            if(timer<=0)
            {
                //Switch the points
                if (partrolPoints[i]!=partrolPoints[partrolPoints.Length-1])
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

    private void MoveCheck()//Check the movement to flip,for animation
    {

    }
}
