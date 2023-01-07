using UnityEngine;
[CreateAssetMenu(menuName = "Voice Lines/First Health Kit Pickup")]
public class FirstHealthKitPickupVoiceLine : VoiceLine
{
    protected override void BindEvents()
    {
        GameState.PlayerPickedUpHealthPackEvent.Event += b => Evaluate();
    }

    protected override bool ConditionCheck()
    {
        return !wasPlayed;
    }
}
