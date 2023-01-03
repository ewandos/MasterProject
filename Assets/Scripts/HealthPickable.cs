using System;

public class HealthPickable : Pickable
{
    public int amount = 10;
    public static event Action pickedHealth;
    
    protected override bool OnPickUp(PlayerManager manager)
    {
        if (manager.health._health >= manager.health._maxHealth) return false; 
        manager.health.Heal(amount);
        return true;
    }
}
