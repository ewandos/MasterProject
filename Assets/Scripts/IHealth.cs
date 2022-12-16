using System;
using UnityEngine;

public abstract class IHealth: MonoBehaviour
{
    [SerializeField] protected int _health = 50;
    [SerializeField] protected int _maxHealth = 50;
    [SerializeField] protected bool _isAlive = true;

    public virtual void TakeDamage(int amount)
    {
        _health = Math.Max(0, _health - amount);

        if (_health == 0)
        {
            _isAlive = false;
            Death();
        }
    }

    public virtual void Heal(int amount)
    {
        _health = Math.Min(_maxHealth, _health + amount);
    }

    public abstract void Death();
}