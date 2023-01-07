using System;

public class HealthPickable : Pickable
{
    public int amount = 10;

    protected override bool OnPickUp(PlayerManager manager)
    {
        if (manager.health._health >= manager.health._maxHealth)
        {
            GameState.PlayerTriedToPickUpHealthPackButIsFullLifeEvent?.Invoke(true);
            return false;
        }
        GameState.PlayerPickedUpHealthPackEvent?.Invoke(true);
        GameState.PlayerPickedUpItem?.Invoke(true);
        manager.health.Heal(amount);
        return true;
    }
}
