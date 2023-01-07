using UnityEngine;

[CreateAssetMenu(menuName = "Voice Lines/First Keycard Pickup")]
public class FirstKeycardPickupVoiceLine : VoiceLine
{
    protected override void BindEvents()
    {
        GameState.PlayerPickedUpKeycardEvent.Event += obj => Evaluate();
    }

    protected override bool ConditionCheck()
    {
        return !wasPlayed;
    }
}
