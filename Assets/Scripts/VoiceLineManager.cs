using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VoiceLineManager : MonoBehaviour
{
    private List<VoiceLine> voiceLines;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        voiceLines = Resources.LoadAll<VoiceLine>("Voicelines").ToList();
        GameState.PlayVoiceLine.Event += PlayVoiceLine;
    }

    private void FixedUpdate()
    {
        foreach (VoiceLine voiceLine in voiceLines)
        {
            if(voiceLine.autoPlay) voiceLine.Evaluate();
        }
    }

    public void PlayVoiceLine(AudioClip audioClip)
    {
        Debug.Log("Play Voice LIne" , audioClip);
        audioSource.PlayOneShot(audioClip);
    }
}
