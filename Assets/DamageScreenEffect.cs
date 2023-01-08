using UnityEngine;

public class DamageScreenEffect : MonoBehaviour
{
    private Material damageMaterial;
    private PlayerHealth playerHealth;

    private void Start()
    {
        damageMaterial = GetComponent<MeshRenderer>().material;
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void Update()
    {
        damageMaterial.SetFloat("_Metallic", 1.0f - playerHealth.GetHealthPercentage());
    }
}
