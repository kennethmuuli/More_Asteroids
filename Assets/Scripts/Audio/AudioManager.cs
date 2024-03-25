using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public static Action<SFXName, int> PlaySFX;
    public static Action<SFXName, int> StopSFX;
    public Sound[] SFXs;

    private void OnEnable() {
        PlaySFX += OnPlaySFX;
        StopSFX += OnStopSFX;
    }
    private void OnDisable() {
        PlaySFX -= OnPlaySFX;
        StopSFX -= OnStopSFX;
    }

    private void Awake() {
        //Make a static instance of the audioManager, to avoid more than one of them in any given scene
        if (instance == null)
        {
            instance = this;
        } else {
            Destroy(gameObject);
        } 

        CreateAudioSources();
    }


    private void Start()
    {
        //Add the don't destory on load property so that the audioManager does not get destoryed between scenes
        DontDestroyOnLoad(gameObject);

        // PlayBackgroundMusic
        // OnPlaySFX(SFXName.BackgroundMusic, gameObject.GetInstanceID());
    }


    private void CreateAudioSources() {
        foreach (Sound sound in SFXs)
        {
            // Use x to loop over audioClips with a separate iterator
            int x = 0;

            for (int i = 0; i < sound.audioClips.Length * sound.poolSize; i++)
            {
                if(x == sound.audioClips.Length) {x = 0;}

                sound.sources.Add(gameObject.AddComponent<AudioSource>());
                sound.sources[i].outputAudioMixerGroup = sound.audioMixerGroup;
                sound.sources[i].clip = sound.audioClips[x];
                sound.sources[i].volume = sound.volume;
                sound.sources[i].pitch = sound.pitch;
                sound.sources[i].loop = sound.loop;
                sound.sources[i].playOnAwake = sound.playOnAwake;
                
                x++;
            } 
        }
    }

    private void OnPlaySFX(SFXName name, int requestorID) {
        Sound foundSound = null;

        foreach (Sound s in SFXs)
        {
            if (s.name == name)
            {
                foundSound = s;
                break;
            }
        }

        var sourceToPlay = foundSound.GetSource(requestorID);

        if (foundSound != null)
        {
            if (foundSound.continuous && !sourceToPlay.isPlaying)
            {
                sourceToPlay.Play();
            } else if (!foundSound.continuous) {
                sourceToPlay.Play();
            }
        }
        else 
        {
            if (foundSound == null)
            {
                Debug.LogWarning("Could not play SFX. No audio source matching name: " + name.ToString() + " was found.");
            }

            if (sourceToPlay.isPlaying) {
                Debug.LogWarning("Could not play SFX, since: " + name.ToString() + " is already playing.");
            }
        }
    }

    private void OnStopSFX(SFXName name, int requestorID)
    {
        Sound foundSound = Array.Find(SFXs, sound => sound.name == name);
        
        if (foundSound != null && foundSound.continuous)
        {
            foundSound.GetSource(requestorID,true).Stop();
        }
        else
        {
            Debug.LogWarning("Could not stop SFX. No audio source matching name: " + name.ToString() + " was found.");
        }
    }

    public void UpdateVolume (SFXName name, float newVolume) {
        if(newVolume < 0) {
            newVolume = 0;
            Debug.LogWarning("Sent in music volume below 0.0, min value 0, set value to 0");
        } else if (newVolume > 1)
        {
            newVolume = 1;
            Debug.LogWarning("Sent in music volume above 1.0, max value 1, set value to 1");
        }

        // Set background music volume
        if (name == SFXName.BackgroundMusic)
        {
            foreach (Sound s in SFXs)
            {
                if (s.name == name)
                {
                    for (int i = 0; i < s.audioClips.Length; i++)
                    {
                        s.volume = newVolume;
                        s.sources[i].volume = newVolume;
                    }
                    return;
                }
            }
        // Set SFXs volume
        } else {
            foreach (Sound s in SFXs)
            {
                if (s.name == SFXName.BackgroundMusic)
                {
                    continue;
                } else {
                    for (int i = 0; i < s.audioClips.Length; i++)
                    {
                        s.volume = newVolume;
                        s.sources[i].volume = newVolume;
                    }
                }
            }
        }
    }

    public float GetCurrentVolume(SFXName name){
        float currentVolume = 0f;
        
        foreach (Sound s in SFXs)
        {
            if (s.name == name)
            {
                currentVolume =  s.volume;
            }
        }
        
        return currentVolume;
    }

}

