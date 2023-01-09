using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = 5;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            if (gameObject.name == "MeleeTrigger")
            {
                gameObject.SetActive(false);
                return;
            }
            Destroy(gameObject);
        }
    }
}