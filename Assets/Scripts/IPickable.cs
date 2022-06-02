using UnityEngine;

public abstract class IPickable : MonoBehaviour
{
    public GameObject model;

    private AudioSource audio;
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
        model.SetActive(false);
        audio.Play();
        Destroy(gameObject, 1);
    }

    protected abstract void OnPickUp(PlayerManager manager);
}
