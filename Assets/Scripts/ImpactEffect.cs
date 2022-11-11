using System;
using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    public float duration;

    private void Update()
    {
        if (duration <= 0f)
            Destroy(gameObject);
        this.duration -= Time.deltaTime;
    }
}
