using System;
using MoreMountains.Feedbacks;

public class PlayerHealth : IHealth
{
    public event Action<int> damageTakenEvent;
    public event Action<int> healedEvent;
    public event Action<int> updatedHealthEvent;
    public event Action deathEvent; 
    
    public MMF_Player onDamageTakenFeedback;
    public MMF_Player onDeathFeedback;
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        if (_health <= 0) return;
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
        onDeathFeedback.PlayFeedbacks();
        deathEvent?.Invoke();
    }
}