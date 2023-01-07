
using System.Collections.Generic;
using UnityEngine;

public abstract class VoiceLine : ScriptableObject
{
    public List<AudioClip> audioClips;
    public bool playRandom;
    public float probability = 1;
    public bool autoPlay;
    protected bool wasPlayed = false;
    private int currentIndex;

    private void OnEnable()
    {
        wasPlayed = false;
        currentIndex = -1;
        BindEvents();
    }

    public void Evaluate()
    {
        if (Random.Range(0f, 1f) >= probability || !ConditionCheck()) return;
        wasPlayed = true;

        if (playRandom)
            currentIndex = Random.Range(0, audioClips.Count);
        else
            currentIndex++;

        GameState.PlayVoiceLine?.Invoke(audioClips[currentIndex]);
    }

    protected virtual void BindEvents() { }
    protected abstract bool ConditionCheck();
}
