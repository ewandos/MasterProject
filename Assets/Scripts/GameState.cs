using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameEvent<bool> GameStartedEvent = new GameEvent<bool>();
    public static GameEvent<AudioClip> PlayVoiceLine = new GameEvent<AudioClip>();

    public static GameEvent<int> PlayerHealedEvent = new GameEvent<int>();
    public static GameEvent<int> PlayerTookDamageEvent = new GameEvent<int>();
    public static GameEvent<int> PlayerHealthUpdatedEvent = new GameEvent<int>();
    public static GameEvent<bool> PlayerDiedEvent = new GameEvent<bool>();
    
    public static GameEvent<bool> PlayerPickedUpGunEvent = new GameEvent<bool>();
    public static GameEvent<bool> PlayerPickedUpKeycardEvent = new GameEvent<bool>();
    public static GameEvent<bool> PlayerPickedUpAmmunitionEvent = new GameEvent<bool>();
    public static GameEvent<bool> PlayerPickedUpHealthPackEvent = new GameEvent<bool>();
    public static GameEvent<bool> PlayerTriedToPickUpHealthPackButIsFullLifeEvent = new GameEvent<bool>();
    public static GameEvent<bool> PlayerPickedUpItem = new GameEvent<bool>();
    
    public static GameEvent<bool> PlayerEncounteredCreepEvent = new GameEvent<bool>();
    public static GameEvent<bool> PlayerViewedWindowEvent = new GameEvent<bool>();
    public static GameEvent<bool> PlayerEncounteredBossEvent = new GameEvent<bool>();
    public static GameEvent<bool> PlayerDefeatedBossEvent = new GameEvent<bool>();
}
