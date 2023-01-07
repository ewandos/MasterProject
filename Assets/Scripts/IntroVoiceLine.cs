using UnityEngine;

[CreateAssetMenu(menuName = "Voice Lines/Intro")]
public class IntroVoiceLine : VoiceLine
{
    public int iteration = 0;
    protected override void BindEvents()
    {
        GameState.GameStartedEvent.Bind(Evaluate);
    }

    protected override bool ConditionCheck()
    {
        Debug.Log(GameState.GameStartedEvent.raisedCount);
        return !wasPlayed && GameState.GameStartedEvent.raisedCount == iteration;
    }
}
