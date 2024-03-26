using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Sound
{
    public SFXName name;
    public AudioMixerGroup audioMixerGroup;
    [Tooltip("Use the pool size to define the number of audio sources to create for each individual sound"), Range(1,100f)] public int poolSize;
    public AudioClip[] audioClips;
    public int iterator = 0;

    [Range(0f,1f)]
    public float volume;
    [Range(.1f,3f)]
    public float pitch;
    [Tooltip("Check if this sound needs the ability to be stopped after being started.")] public bool continuous;
    public bool loop;
    public bool playOnAwake;
    public bool fadeOutOnStop;
    [Range(0.1f, 2f)]public float fadeOutDuration;

    [HideInInspector]
    public List<AudioSource> sources = new List<AudioSource>();
    public Dictionary<int, AudioSource> playingSources = new Dictionary<int, AudioSource>();

    public AudioSource GetSource(int requestorID, bool getStop = false){
        if (getStop)
        {
            return LoopOverPlayingSources(requestorID);
        } else {
            return LoopOverSources(requestorID);
        }
    }

    private AudioSource LoopOverSources(int requestorID){
        AudioSource sourceToPlay = null;

        if (iterator >= sources.Count) {
            iterator = 0;
        }
        
        sourceToPlay = sources[iterator];

        if (!continuous || playingSources.ContainsKey(requestorID))
        {
            iterator++;
        }

        // Register sources if they can be cancelled
        if (continuous && !playingSources.ContainsKey(requestorID))
        {
            playingSources.Add(requestorID,sourceToPlay);
        } else if (continuous && playingSources.ContainsKey(requestorID)) {
            sourceToPlay = playingSources[requestorID];
        }
        return sourceToPlay;
    }

    private AudioSource LoopOverPlayingSources(int requestorID){
        AudioSource sourceToStop = null;

        if (playingSources.ContainsKey(requestorID)) {
            sourceToStop = playingSources[requestorID];
        } else {
            Debug.LogWarning("Cant return source to stop. No source with requestorID: " + requestorID + " has been registered for this sound.");
        }

        return sourceToStop != null ? sourceToStop : null;
    }
}
