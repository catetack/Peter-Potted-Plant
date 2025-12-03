using UnityEngine;


public class baseTimer : MonoBehaviour
{
    public const float MAX_TIME = 30f;

    private float duration = MAX_TIME;
    public float currentTime;
    public bool isRunning = false;
    public bool isEnd=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = duration;
    }

    // Update is called once per frame
    void Update()
    {
        TimeRun();

        TimeCheck();
    }

    public void TimeRun()
    {
        if (!isRunning)
        {
            return;
        }

        currentTime -= Time.deltaTime;
    }

    public void TimeCheck()
    {
        if (currentTime <= 0 && !isEnd)
        {
            currentTime = 0;
            isRunning = false;
            isEnd = true;
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void ResetTimer()
    {
        currentTime = duration;
        isRunning = false;
    }

    public void Pause()
    {
        isRunning = false;
    }
}
