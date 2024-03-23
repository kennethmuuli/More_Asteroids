using UnityEngine.Audio;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private float _currentMusicVolume = 0.5f;
    [Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("Background Music")]
    [SerializeField] private AudioClip _backgroundMusic;

    private AudioClip _SFXToPlay;
    public static Action<SFXName> playSFX;
    [Header("Ship sounds")]
    [SerializeField] private AudioClip[] _projectileShootSound;
    private int _projectileShootSoundIterator;
    [SerializeField] private AudioClip _laserSound, _accelerateSound, _boostSound;
    [Header("Destructibles sounds")]
    [SerializeField] private AudioClip _asteroidDestructionSound, _hitSuccessSound, _hitFailSound;
    [Header("UI sounds")]
    [SerializeField] private AudioClip _uiMove, _uiSelect;




    public float GetCurrentMusicVolume {
        get {return _currentMusicVolume;}
    }
    private void Awake() {
        //Make a static instance of the audioManager, to avoid more than one of them in any given scene
        if (instance == null)
        {
            instance = this;
        } else {
            Destroy(gameObject);
        } 
    }

    private void OnEnable() {
        playSFX += OnPlaySoundEffect;
    }
    private void OnDisable() {
        playSFX -= OnPlaySoundEffect;
    }

    private void Start()
    {
        //Add the don't destory on load property so that the audioManager does not get destoryed between scenes
        DontDestroyOnLoad(gameObject);
        PlayBackgroundMusic();
    }
    public void UpdateMusicVolume (float newVolume) {
        if(newVolume < 0) {
            newVolume = 0;
            Debug.LogWarning("Sent in music volume below 0.0, min value 0, set value to 0");
        } else if (newVolume > 1)
        {
            newVolume = 1;
            Debug.LogWarning("Sent in music volume above 1.0, max value 1, set value to 1");
        }
        _currentMusicVolume = newVolume;
        musicSource.volume = newVolume;
    }

    private void PlayBackgroundMusic() {
        musicSource.clip = _backgroundMusic;
        musicSource.Play();
    }

    // Create a new case for each audio clip and then simply invoke the event from the right place with 'AudioManager.playSFX?.Invoke(<SFXName here>);
    private void OnPlaySoundEffect (SFXName SFXName) {

        switch (SFXName)
        {
            case SFXName.ShootProjectile:
                _SFXToPlay = loopClipsArray(_projectileShootSound, _projectileShootSoundIterator);
                break;
            case SFXName.ShootLaser:
                _SFXToPlay = _laserSound;
                break;
            case SFXName.AstroidDestruction:
                _SFXToPlay = _asteroidDestructionSound;
                break;
            default:
                break;
        }

        SFXSource.clip = _SFXToPlay;
        SFXSource.Play();
    }

    private AudioClip loopClipsArray(AudioClip[] audioClips, int arrayIterator) {
        
        if (audioClips.Length < arrayIterator)
        {
            arrayIterator = 0;
        }
        AudioClip clipToReturn = audioClips[arrayIterator];

        arrayIterator++;
        
        return clipToReturn;
    }
}

//If you want to add a new SFX name, simply add it here, this is used for referencing audio clips, which are all added to this script
public enum SFXName {
    ShootProjectile,
    ShootLaser,
    AstroidDestruction
}