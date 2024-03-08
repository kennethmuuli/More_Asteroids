using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private Button gameOverMenuInitial;
    // Start is called before the first frame update
    private void OnEnable() {
        GameManager.OnUpdateGameState += ToggleGameOverMenu;
    }

    private void OnDisable() {
        GameManager.OnUpdateGameState -= ToggleGameOverMenu;
    }

    private void ToggleGameOverMenu(GameState stateToCheck) {
        if (stateToCheck == GameState.GameOver) {
            gameOverMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(gameOverMenuInitial.gameObject);
        }
    }

    public void ContinueToMainMenu() {
        SceneManager.LoadScene(0);
    }

}
