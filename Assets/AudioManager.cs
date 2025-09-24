using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //one source for music, one for other sound effects
    [SerializeField] AudioSource bgMusicSource;
    [SerializeField] AudioSource soundEffectsSource;

    //clips for each sound effect
    public AudioClip bgMusic;
    public AudioClip jump;

    //plays background music at start
    private void Start()
    {
        bgMusicSource.clip = bgMusic;
        bgMusicSource.Play();
    }

    //for the sound effects
    public void playSoundEffect(AudioClip soundEffect)
    {
        soundEffectsSource.PlayOneShot(soundEffect);
    }
}
