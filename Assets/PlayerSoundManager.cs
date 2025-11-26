using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [Header("Sound Effects")]
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    public AudioClip sound4;

    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
