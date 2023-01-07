using UnityEngine;

[CreateAssetMenu(menuName = "Voice Lines/First Ammunition Pickup")]
public class FirstAmmunitionPickupVoiceLine : VoiceLine
{
    protected override void BindEvents()
    {
        GameState.PlayerPickedUpAmmunitionEvent.Bind(Evaluate);
    }

    protected override bool ConditionCheck()
    {
        return !wasPlayed;
    }
}
