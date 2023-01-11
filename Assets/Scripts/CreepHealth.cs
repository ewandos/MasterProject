using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreepHealth : IHealth
{
    public int healthRange = 1;
    public GameObject corpseGO;

    private void Start()
    {
        _maxHealth = Random.Range(_health - healthRange, _health + healthRange);
        _health = _maxHealth;
    }

    public override void Death()
    {
        Instantiate(corpseGO, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}