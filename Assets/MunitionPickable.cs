using UnityEngine;

public class MunitionPickable : MonoBehaviour
{
    public int amount = 5;
    private void OnTriggerEnter(Collider other)
    {
        PlayerManager manager = other.GetComponent<PlayerManager>();
        if (manager == null) return;
        manager.gun.AddAmmo(amount);
        Destroy(gameObject);
    }
}
