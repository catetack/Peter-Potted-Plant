using UnityEngine;

public class baseTimer : MonoBehaviour
{
    public float duration = 3f;
    public float currentTime;
    public bool isRunning = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning)
        {
            return;
        }

        currentTime -= Time.deltaTime;

        if (isEnd())
        {
            currentTime = 0;
            isRunning = false;
        }
    }

    public void StartTimer()
    {
        currentTime = duration;
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

    public bool isEnd()
    {
        if(currentTime<=0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
