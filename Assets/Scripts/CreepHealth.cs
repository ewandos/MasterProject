using UnityEngine;

public class CreepHealth : IHealth
{
    public GameObject corpseGO;
    public override void Death()
    {
        Instantiate(corpseGO, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}