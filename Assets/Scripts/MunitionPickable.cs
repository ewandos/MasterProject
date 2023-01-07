using System;

public class MunitionPickable : Pickable
{
    public int amount = 5;

    protected override bool OnPickUp(PlayerManager manager)
    {
        GameState.PlayerPickedUpAmmunitionEvent?.Invoke(true);
        GameState.PlayerPickedUpItem?.Invoke(true);
        manager.gun.AddAmmo(amount);
        return true;
    }
}
