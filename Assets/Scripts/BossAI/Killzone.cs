using UnityEngine;

public class Killzone : MonoBehaviour
{
    private HealthSystem healthSystem;
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerHealth>().TakeDamage(10000);
    }
}