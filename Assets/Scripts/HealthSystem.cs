using System;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private int health = 50;

    [SerializeField] 
    private int maxHealth = 50;

    [SerializeField] public bool bossDead = false;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private bool isPlayer;

    public UnityAction onDamageTaken;
    public void Start()
    {
        health = maxHealth;
        if (GetComponent<PlayerMovement>())
        {
            isPlayer = true;
            text.text = health.ToString();
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (isPlayer)
        {
            text.text = health.ToString();
        }
        
        if (health <= 0)
        {
            if (isPlayer)
            {
                text.text = "0";
            }
            Death();
        }
    }

    void Death()
    {
        if (isPlayer)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else if (GetComponent<GOAPPlanner>())
        {
            //enable you win screen
            bossDead = true;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddHealth(int amount)
    {
        health += amount;
        if (health > maxHealth)
            health = maxHealth;
    }
}