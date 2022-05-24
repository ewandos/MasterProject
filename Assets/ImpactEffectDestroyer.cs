using UnityEngine;

public class ImpactEffectDestroyer : MonoBehaviour
{
    public float duration;

    private void Update()
    {
        if (duration <= 0f)
            Destroy(gameObject);
        this.duration -= Time.deltaTime;
    }
}
