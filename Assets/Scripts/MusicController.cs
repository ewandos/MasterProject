using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip suspenseMusic;
    public AudioClip bossMusic;
    public PlayerHealth playerHealth;
    private bool _playedSuspenseMusic = false;
    
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerHealth.damageTakenEvent += i =>
        {
            if (_playedSuspenseMusic) return;
            _playedSuspenseMusic = true;
            PlaySuspenseMusic();
        } ;
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
