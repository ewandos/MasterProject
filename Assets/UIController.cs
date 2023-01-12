using UnityEngine;

public class UIController : MonoBehaviour
{
    public Gun gun;
    public UIAmmo uiAmmo;
    public GlowstickController glowstickController;
    public PlayerHealth health;
    public HealthUI uiHealth;
    public GlowstickUI glowstickUI;

    public void Start()
    {
        uiAmmo.Initialize(gun);
        uiHealth.Initialize(health);
        glowstickUI.Initialize(glowstickController);
    }
}
