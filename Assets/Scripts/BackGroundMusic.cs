using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip musicClip; // Song

    void Start()
    {
        audioSource.clip = musicClip;
        audioSource.Play();
    }
}
