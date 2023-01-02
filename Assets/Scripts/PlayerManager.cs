using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Gun gun;
    public PlayerHealth health;
    public Keychain keychain;

    private void Start()
    {
        gun.enabled = GameSettings.playerStartsWithGun;
    }
}
