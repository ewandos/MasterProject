using UnityEngine;

[CreateAssetMenu(menuName = "Voice Lines/Humming")]
public class HummingVoiceLine : VoiceLine
{
    protected override bool ConditionCheck()
    {
        return !wasPlayed &&
               GameState.GameStartedEvent.TimePassedSinceLastRaise(60f) &&
               GameState.PlayerTookDamageEvent.TimePassedSinceLastRaise(15f) &&
               GameState.PlayVoiceLine.TimePassedSinceLastRaise(15f);
    }
}
