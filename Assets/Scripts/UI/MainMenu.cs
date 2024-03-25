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
    [SerializeField] private Slider musicVolumeSlider;

    private void Start() {
        // Read in last value from audioManager, otherwise every return to main menu resets volume
        // musicVolumeSlider.value = AudioManager.instance.GetCurrentVolume(SFXName.BackgroundMusic);
        AudioManager.PlaySFX?.Invoke(SFXName.BackgroundMusic,gameObject.GetInstanceID());
        Time.timeScale = 1f;
    }

    public void SetMusicVolume()
    {
        float volume = musicVolumeSlider.value;
        // AudioManager.instance.UpdateVolume(SFXName.BackgroundMusic, volume);
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
        SetMusicVolume();   
    }

    private void DeactivateMenus () {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
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
