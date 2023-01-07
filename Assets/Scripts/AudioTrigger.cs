using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    public void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        PlaySound();
    }
}
