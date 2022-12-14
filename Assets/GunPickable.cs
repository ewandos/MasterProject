public class GunPickable : Pickable
{
    protected override void OnPickUp(PlayerManager manager)
    {
        manager.gun.enabled = true;
    }
}
