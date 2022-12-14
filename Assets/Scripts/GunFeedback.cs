using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunFeedback : MonoBehaviour
{
    private Light light;
    public float lightDuration = 0.1f;
    public float lightRandomAngleRange = 0.1f;
    public List<AudioClip> shotSounds;
    private float lightCountdown = 0;

    private AudioSource audio;

    private ParticleSystem particles;

    private void Start()
    {
        light = GetComponent<Light>();
        audio = GetComponent<AudioSource>();
        particles = GetComponent<ParticleSystem>();
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

        audio.clip = shotSounds[Random.Range(0, shotSounds.Count)];
        audio.Play();
        
        particles.Play();
    }
}
