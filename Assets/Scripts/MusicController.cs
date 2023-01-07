using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip suspenseMusic;
    public PlayerHealth playerHealth;
    private bool _playedSuspenseMusic = false;
    
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerHealth.damageTakenEvent += i =>
        {
            if (_playedSuspenseMusic || playerHealth._health / playerHealth._maxHealth > 0.2f) return;
            _playedSuspenseMusic = true;
            audioSource.clip = suspenseMusic;
            audioSource.Play();
        };
    }
}
