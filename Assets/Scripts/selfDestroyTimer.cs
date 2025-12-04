using UnityEngine;

public class selfDestroyTimer : baseTimer
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeRun();

        if (currentTime<=0)
        {
                Destroy(gameObject);
        }
    }
}
