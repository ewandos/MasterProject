using UnityEngine;


public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private float Health = 50f;

    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}