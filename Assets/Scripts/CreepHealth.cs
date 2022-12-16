public class CreepHealth : IHealth
{
    public override void Death()
    {
        Destroy(gameObject);
    }
}