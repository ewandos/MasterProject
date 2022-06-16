public class MunitionPickable : Pickable
{
    public int amount = 5;

    protected override void OnPickUp(PlayerManager manager)
    {
        manager.gun.AddAmmo(amount);
    }
}
