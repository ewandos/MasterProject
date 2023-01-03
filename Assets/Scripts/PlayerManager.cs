using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Gun gun;
    public PlayerHealth health;
    public Keychain keychain;
    public GameSettings gameSettings;

    private void Start()
    {
        gun.enabled = gameSettings.playerStartsWithGun;
    }
}
