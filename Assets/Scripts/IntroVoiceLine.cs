using UnityEngine;

[CreateAssetMenu(menuName = "Voice Lines/Intro")]
public class IntroVoiceLine : VoiceLine
{
    public int iteration = 0;
    protected override void BindEvents()
    {
        GameState.GameStartedEvent.Event += b => Evaluate();
    }

    protected override bool ConditionCheck()
    {
        return !wasPlayed && GameState.GameStartedEvent.raisedCount == iteration;
    }
}
