using UnityEngine.Audio;
using UnityEngine;
using UnityEditor.Timeline.Actions;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;
    [Range(0f, 1f)]

    public float volume;
    [Range(.1f, 3f)]

    // Not sure if needed
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

}
