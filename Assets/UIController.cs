using UnityEngine;

public class UIController : MonoBehaviour
{
    public Gun gun;
    public UIAmmo uiAmmo;

    public PlayerHealth health;
    public HealthUI uiHealth;

    public void Start()
    {
        uiAmmo.Initialize(gun);
        uiHealth.Initialize(health);
    }
}
