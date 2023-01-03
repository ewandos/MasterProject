using System;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using Random = UnityEngine.Random;

public class ImpactEffect : MonoBehaviour
{
    public List<AudioClip> impactSounds;
    public float duration;

    private void Start()
    {
        GetComponent<AudioSource>().clip = impactSounds[Random.Range(0, impactSounds.Count)];
        GetComponent<AudioSource>().Play();
        GetComponent<MMF_Player>().PlayFeedbacks();
    }

    private void Update()
    {
        if (duration <= 0f)
            Destroy(gameObject);
        this.duration -= Time.deltaTime;
    }
}
