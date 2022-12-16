public class HealthPickable : Pickable
{
    public int amount = 10;

    protected override void OnPickUp(PlayerManager manager)
    {
        manager.health.Heal(amount);
    }
}
