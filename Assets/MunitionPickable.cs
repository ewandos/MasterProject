using UnityEngine;

public class MunitionPickable : MonoBehaviour
{
    public GameObject model;
    public int amount = 5;
    
    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerManager manager = other.GetComponent<PlayerManager>();
        if (manager == null) return;
        manager.gun.AddAmmo(amount);
        model.SetActive(false);
        audio.Play();
        Destroy(gameObject, 1);
    }
}
