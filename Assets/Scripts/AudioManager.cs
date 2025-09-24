using System.Runtime.CompilerServices;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource backgroundSrc;
    [SerializeField] AudioSource soundEffectsSrc;

    public AudioClip backgroundNoise;
    public AudioClip jump;
    public AudioClip jumpCharge;
    public AudioClip moving;
    public AudioClip falling;
    public AudioClip respawn;

    private void Start()
    {
        backgroundSrc.clip = backgroundNoise;
        backgroundSrc.Play();
    }

    public void PlaySoundEffect(AudioClip soundEffect)
    {
        soundEffectsSrc.PlayOneShot(soundEffect);
    }
}