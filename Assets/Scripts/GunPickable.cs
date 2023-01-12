using System;

public class GunPickable : Pickable
{
    protected override bool OnPickUp(PlayerManager manager)
    {
        manager.gun.enabled = true;
        GameState.PlayerPickedUpGunEvent?.Invoke(true);
        GameState.PlayerPickedUpItem?.Invoke(true);
        return true;
    }
}
