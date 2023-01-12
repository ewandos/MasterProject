using System;

public class MunitionPickable : Pickable
{
    public int munitionAmount = 5;
    public int glowstickAmount = 2;

    protected override bool OnPickUp(PlayerManager manager)
    {
        GameState.PlayerPickedUpAmmunitionEvent?.Invoke(true);
        GameState.PlayerPickedUpItem?.Invoke(true);
        manager.gun.AddAmmo(munitionAmount);
        manager.glowstickController.AddGlowstick(glowstickAmount);
        return true;
    }
}
