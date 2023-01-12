using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameSettings gameSettings;
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
        
        uiAmmo.gameObject.SetActive(gameSettings.playerStartsWithGun);
        uiHealth.gameObject.SetActive(gameSettings.playerStartsWithGun);
        glowstickUI.gameObject.SetActive(gameSettings.playerStartsWithGun);

        GameState.PlayerPickedUpGunEvent.Event += b =>
        {
            uiAmmo.gameObject.SetActive(true);
            uiHealth.gameObject.SetActive(true);
            glowstickUI.gameObject.SetActive(true);
        };
    }
}
