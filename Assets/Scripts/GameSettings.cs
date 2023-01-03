using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    public bool playerStartsWithGun = false;
    public bool peacefulMode = false;
    public bool openLevel = true;
}
