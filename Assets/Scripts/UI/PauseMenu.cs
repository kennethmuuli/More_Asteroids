using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button pauseMenuInitial;
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
            EventSystem.current.SetSelectedGameObject(pauseMenuInitial.gameObject);
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
