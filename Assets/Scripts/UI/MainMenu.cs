using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Button mainMenuInitial;
    [Header("Options Menu")]
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private Button optionsMenuInitial;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicVolumeSlider, SFXVolumeSlider;
    [Header("Controls Menu")]
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private Button controlsMenuInitial;

    private void Start() {
        LoadSettingsPrefs();
        Time.timeScale = 1f;
    }

    private void LoadSettingsPrefs() {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            var loadedValue = PlayerPrefs.GetFloat("musicVolume",musicVolumeSlider.value);
            audioMixer.SetFloat("Music", Mathf.Log10(loadedValue) * 20); 
            musicVolumeSlider.value = loadedValue;
        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            var loadedValue = PlayerPrefs.GetFloat("SFXVolume",SFXVolumeSlider.value);
            audioMixer.SetFloat("SFX", Mathf.Log10(loadedValue) * 20);
            SFXVolumeSlider.value = loadedValue;
        }
    }
 
    public void SetMusicVolume()
    {
        float volume = musicVolumeSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);

    }
    public void SetSFXVolume()
    {
        float volume = SFXVolumeSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ActivateMainMenu () {
        DeactivateMenus();
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(mainMenuInitial.gameObject);
    }

    public void ActivateOptionsMenu () {
        DeactivateMenus();
        optionsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(optionsMenuInitial.gameObject);
    }
    public void ActivateControlsMenu () {
        DeactivateMenus();
        controlsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(controlsMenuInitial.gameObject);
    }

    private void DeactivateMenus () {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        // for testing
        Debug.Log("QUIT!");
#endif

        Application.Quit();
    }
}
