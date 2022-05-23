using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MuzzleFlashController : MonoBehaviour
{
    private Light light;
    public float lightDuration = 0.1f;
    public float lightRandomAngleRange = 0.1f;
    private float lightCountdown = 0;

    private AudioSource audio;
    public float soundRandomPitchRange = 0.1f;

    private void Start()
    {
        light = GetComponent<Light>();
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (lightCountdown > 0f)
        {
            lightCountdown -= Time.deltaTime;
        }
        else if (light.enabled)
        {
            light.enabled = false;
        }
    }

    public void Effect()
    {
        lightCountdown = lightDuration;
        light.enabled = true;

        float min = 130 - lightRandomAngleRange;
        float max = 130 + lightRandomAngleRange;
        float randomAngle = Random.Range(min, max);
        light.spotAngle = randomAngle;

        audio.pitch = 1 + Random.Range(-soundRandomPitchRange, soundRandomPitchRange / 2);
        audio.Play();
    }
}
