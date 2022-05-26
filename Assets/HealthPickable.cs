using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickable : MonoBehaviour
{
    public int amount = 5;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Not Implemented Health System for Player");
        Destroy(gameObject);
    }
}
