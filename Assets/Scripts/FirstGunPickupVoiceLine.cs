using UnityEngine;

[CreateAssetMenu(menuName = "Voice Lines/First Gun Pickup")]
public class FirstGunPickupVoiceLine : VoiceLine
{
    protected override void BindEvents()
    {
        GameState.PlayerPickedUpGunEvent.Bind(Evaluate);
    }

    protected override bool ConditionCheck()
    {
        return !wasPlayed;
    }
}
