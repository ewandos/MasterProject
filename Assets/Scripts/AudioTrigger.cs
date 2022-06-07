using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    private void OnTriggerEnter(Collider other)
    {
        if (audioSource != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            Destroy(gameObject);
        }
    }
}
