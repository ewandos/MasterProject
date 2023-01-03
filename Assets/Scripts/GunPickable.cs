using System;

public class GunPickable : Pickable
{
    public static event Action pickedGun;
    
    protected override bool OnPickUp(PlayerManager manager)
    {
        manager.gun.enabled = true;
        return true;
    }
}
