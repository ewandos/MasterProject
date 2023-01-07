using UnityEngine;

[CreateAssetMenu(menuName = "Voice Lines/Health Kit Pickup But Full Life")]
public class PlayerTriedToPickUpHealthPackFullLifeVoiceLine : VoiceLine
{
    protected override void BindEvents()
    {
        GameState.PlayerTriedToPickUpHealthPackButIsFullLifeEvent.Bind(Evaluate);
    }

    protected override bool ConditionCheck()
    {
        return !wasPlayed && GameState.PlayerTriedToPickUpHealthPackButIsFullLifeEvent.wasRaisedOnce;
    }
}
