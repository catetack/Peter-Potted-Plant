using UnityEngine;

public class setFramerate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //QualitySettings.vSyncCount = 0; // Set vSyncCount to 0 so that using .targetFrameRate is enabled.
        //Application.targetFrameRate = 12;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(QualitySettings.vSyncCount + " " + Application.targetFrameRate);
    }
}
