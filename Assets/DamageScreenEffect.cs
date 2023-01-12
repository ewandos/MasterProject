using UnityEngine;

public class DamageScreenEffect : MonoBehaviour
{
    private Material damageMaterial;
    private PlayerHealth playerHealth;

    private void Start()
    {
        damageMaterial = GetComponent<MeshRenderer>().material;
        playerHealth = FindObjectOfType<PlayerHealth>();
        GameState.PlayerHealthUpdatedEvent.Event += UpdateDamageOverlay;
    }

    private void UpdateDamageOverlay(int health)
    {
        Color color = damageMaterial.color;
        color.a = Mathf.Clamp( 1 - playerHealth.GetHealthPercentage(), 0, 1 );
        damageMaterial.color = color;
    }
}
