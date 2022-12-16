using System;
using MoreMountains.Feedbacks;

public class PlayerHealth : IHealth
{
    public event Action<int> damageTakenEvent;
    public event Action<int> healedEvent;
    public event Action<int> updatedHealthEvent;
    public event Action deathEvent;
    
    public MMF_Player onDamageTakenFeedback;
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        damageTakenEvent?.Invoke(amount);
        updatedHealthEvent?.Invoke(_health);
        onDamageTakenFeedback.PlayFeedbacks();
    }

    public override void Heal(int amount)
    {
        base.Heal(amount);
        healedEvent?.Invoke(amount);
        updatedHealthEvent?.Invoke(_health);
    }

    public override void Death()
    {
        Destroy(gameObject);
    }
}