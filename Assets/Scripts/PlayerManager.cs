using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool startWithGun = true;
    public Gun gun;
    public PlayerHealth health;
    public Keychain keychain;

    private void Start()
    {
        gun.enabled = startWithGun;
    }
}
