using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public float delay = 0f;
    public bool playOneShot;

    public void PlaySound()
    {
        if (playOneShot)
        {
            audioSource.PlayOneShot(audioClip);
            Destroy(gameObject);
        }
        else
        {
            audioSource.clip = audioClip;
            audioSource.PlayDelayed(delay);
        }
        
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Invoke(nameof(PlaySound), delay);
    }
}
