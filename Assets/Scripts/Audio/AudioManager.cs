using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Audio;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public static Action<SFXName, int> PlaySFX;
    public static Action<SFXName, int> StopSFX;
    public Sound[] SFXs;

    private Dictionary<AudioSource, float> originalVolumes = new Dictionary<AudioSource, float>();

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
        PlaySFX?.Invoke(SFXName.BackgroundMusic,gameObject.GetInstanceID());
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

        AudioSource sourceToStop = foundSound.GetSource(requestorID,true);
        
        if (foundSound != null && foundSound.continuous)
        {
            if (foundSound.fadeOutOnStop)
            {
                StartCoroutine(FadeOut(sourceToStop, foundSound.fadeOutDuration));
            } else {sourceToStop.Stop();}
        }
        else
        {
            Debug.LogWarning("Could not stop SFX. No audio source matching name: " + name.ToString() + " was found.");
        }
    }

    private IEnumerator FadeOut(AudioSource source, float duration)
    {
        if (!originalVolumes.ContainsKey(source))
        {
            originalVolumes.Add(source, source.volume);
        }
        
        float startVolume = source.volume;

        float timeElapsed = 0;

        while (timeElapsed < duration){
            float t = timeElapsed / duration;
            source.volume = Mathf.Lerp(source.volume,0,t);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        source.Stop();
        source.volume = originalVolumes[source];
    }

}

