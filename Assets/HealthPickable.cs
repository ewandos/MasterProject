using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickable : MonoBehaviour
{
    public int amount = 10;
    private void OnTriggerEnter(Collider other)
    {
        PlayerManager manager = other.GetComponent<PlayerManager>();
        if (manager == null) return;
        manager.health.AddHealth(amount);
        Destroy(gameObject);
    }
}
