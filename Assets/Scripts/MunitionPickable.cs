using System;

public class MunitionPickable : Pickable
{
    public int amount = 5;
    public static event Action pickedMunition;
    
    protected override bool OnPickUp(PlayerManager manager)
    {
        manager.gun.AddAmmo(amount);
        return true;
    }
}
