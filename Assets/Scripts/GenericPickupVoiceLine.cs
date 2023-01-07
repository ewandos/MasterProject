using UnityEngine;

[CreateAssetMenu(menuName = "Voice Lines/Generic Pickup")]
public class GenericPickupVoiceLine  : VoiceLine
{
    protected override void BindEvents()
    {
        GameState.PlayerPickedUpItem.Bind(Evaluate);
    }

    protected override bool ConditionCheck()
    {
        return GameState.PlayerPickedUpAmmunitionEvent.wasRaisedOnce &&
               GameState.PlayerPickedUpKeycardEvent.wasRaisedOnce &&
               GameState.PlayerPickedUpGunEvent.wasRaisedOnce &&
               GameState.PlayerTookDamageEvent.TimePassedSinceLastRaise(10f, true);
    }
}