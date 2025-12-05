using UnityEngine;

public class armRotation : MonoBehaviour
{
    playerStateManager PlayerState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerState = GameObject.FindWithTag("Player").GetComponent<playerStateManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
