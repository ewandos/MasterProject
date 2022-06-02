public class HealthPickable : IPickable
{
    public int amount = 10;

    protected override void OnPickUp(PlayerManager manager)
    {
        manager.health.AddHealth(amount);
    }
}
