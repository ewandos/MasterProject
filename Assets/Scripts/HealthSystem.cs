using System;
using UnityEngine;


public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private int health = 50;

    [SerializeField] 
    private int maxHealth = 50;

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }

    public void AddHealth(int amount)
    {
        health += amount;
        if (health > maxHealth)
            health = maxHealth;
    }
}