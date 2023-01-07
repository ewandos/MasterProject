using UnityEngine;

[CreateAssetMenu(menuName = "Voice Lines/First Gun Pickup")]
public class FirstGunPickupVoiceLine : VoiceLine
{
    protected override void BindEvents()
    {
        GameState.PlayerPickedUpGunEvent.Event += b => Evaluate();
    }

    protected override bool ConditionCheck()
    {
        return !wasPlayed;
    }
}
