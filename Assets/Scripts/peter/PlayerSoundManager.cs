using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [Header("Sound Effects")]
    public AudioClip playerFailSound;
    public AudioClip playerHealSound;
    public AudioClip worldSong;
    public AudioClip worldSongFast;

    private AudioSource audioSource;
    private bool wasDownedLastFrame = false;
    
    // For overlapping heal sounds
    private AudioSource healAudioSource;

    InputSystem_Actions inputActions;

    float reviveLeft;
    float reviveRight;
    playerStateManager PlayerState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        assignObjects();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // Create a separate AudioSource for heal sounds that can overlap
        healAudioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        reviveInput();
        if (PlayerState.isDowned){
            if (reviveLeft > 0.9f || reviveRight > 0.9f)
            {
                if (playerHealSound != null)
                {
                    float gain = Mathf.Clamp01(100 / PlayerState.health + 0.1f);
                    healAudioSource.pitch = 1.0f + PlayerState.health/10.0f;
                    healAudioSource.PlayOneShot(playerHealSound, 0.5f);
                }
                else
                {
                }
            }
        }
        // Only play when state changes from not downed to downed
        if (PlayerState.isDowned && !wasDownedLastFrame)
        {
            if (playerFailSound != null)
            {
                audioSource.PlayOneShot(playerFailSound);
            }
            else
            {
            }
        }
        
        wasDownedLastFrame = PlayerState.isDowned;
    }

    void assignObjects()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
        PlayerState = GetComponentInParent<playerStateManager>();
        if (PlayerState == null)
        {
            Debug.LogError("playerStateManager script not found on 'PlayerState' parent.");
        }
    }

    void reviveInput()
    {
        reviveLeft = inputActions.Player.ReviveLeft.ReadValue<float>();
        reviveRight = inputActions.Player.ReviveRight.ReadValue<float>();
    }
}
