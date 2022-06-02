using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardPickable : MonoBehaviour
{
    public GameObject model;
    public int code = 0;
    
    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerManager manager = other.GetComponent<PlayerManager>();
        if (manager == null) return;
        manager.keychain.AddCode(code);
        model.SetActive(false);
        audio.Play();
        Destroy(gameObject, 1);
    }
}
