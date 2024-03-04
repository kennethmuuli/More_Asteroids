using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    // Start is called before the first frame update
    private void OnEnable() {
        GameManager.OnUpdateGameState += TogglePauseMenu;
    }

    private void OnDisable() {
        GameManager.OnUpdateGameState -= TogglePauseMenu;
    }

    private void TogglePauseMenu(GameState stateToCheck) {
        if (stateToCheck == GameState.Pause) {
            pauseMenu.SetActive(true);
        }
    }

    public void QuitGame() {
        SceneManager.LoadScene(0);
    }

    public void ContinueGame() {
        GameManager.instance.UpdateGameState(GameState.Play);
        pauseMenu.SetActive(false);
    }

}
