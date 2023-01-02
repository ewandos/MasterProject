using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(SphereCollider))]
public abstract class Pickable : MonoBehaviour
{
    private new AudioSource audio;
    private bool isCollected;
    
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;
        PlayerManager manager = other.GetComponent<PlayerManager>();
        if (manager == null) return;
        OnPickUp(manager);
        isCollected = true;
        audio.Play();
        Destroy(gameObject, 0.2f);
    }

    protected abstract void OnPickUp(PlayerManager manager);
}
