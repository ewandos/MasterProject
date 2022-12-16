using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DoorAudioController : MonoBehaviour
{
    public AudioClip open;
    public AudioClip close;
    public AudioClip locked;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    public void PlayOpen()
    {
        audioSource.clip = open;
        audioSource.Play();
    }
    
    public void PlayClose()
    {
        audioSource.clip = close;
        audioSource.Play();
    }
    
    public void PlayLocked()
    {
        audioSource.clip = locked;
        audioSource.Play();
    }
}
