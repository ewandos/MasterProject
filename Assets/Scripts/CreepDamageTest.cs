using UnityEngine;

public class CreepDamageTest : MonoBehaviour
{
    public int damageOnCollision = 25;
    private void OnCollisionEnter(Collision collision)
    {
        PlayerManager playerManager = collision.gameObject.GetComponent<PlayerManager>();
        if (playerManager == null) return;
        playerManager.health.TakeDamage(damageOnCollision);
    }
}
