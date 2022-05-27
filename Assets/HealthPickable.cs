using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickable : MonoBehaviour
{
    public GameObject model;
    public int amount = 10;

    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerManager manager = other.GetComponent<PlayerManager>();
        if (manager == null) return;
        manager.health.AddHealth(amount);
        model.SetActive(false);
        audio.Play();
        Destroy(gameObject, 1);
    }
}
