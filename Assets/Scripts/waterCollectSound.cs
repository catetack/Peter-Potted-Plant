using UnityEngine;

public class waterCollectSound : MonoBehaviour
{
    private AudioSource collectSound;

    private void Start()
    {
        collectSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            collectSound.Play();
        }
    }
}
