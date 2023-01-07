using System;

public class GunPickable : Pickable
{
    protected override bool OnPickUp(PlayerManager manager)
    {
        GameState.PlayerPickedUpGunEvent?.Invoke(true);
        GameState.PlayerPickedUpItem?.Invoke(true);
        manager.gun.enabled = true;
        return true;
    }
}
