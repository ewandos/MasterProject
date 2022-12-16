using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip suspenseMusic;
    public AudioClip bossMusic;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySuspenseMusic()
    {
        audioSource.clip = suspenseMusic;
        audioSource.Play();
    }

    public void PlayBossMusic()
    {
        audioSource.clip = bossMusic;
        audioSource.Play();
    }
}
