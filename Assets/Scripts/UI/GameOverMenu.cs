using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private Button gameOverMenuInitial;
    [SerializeField] private TextMeshProUGUI scoreValue;
    // Start is called before the first frame update
    private void OnEnable() {
        GameManager.OnUpdateGameState += ToggleGameOverMenu;
        Scoretracker.PublishScore += ShowFinalScore;
    }

    private void OnDisable() {
        GameManager.OnUpdateGameState -= ToggleGameOverMenu;
        Scoretracker.PublishScore -= ShowFinalScore;
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

    private void ShowFinalScore(int finalScore) {
        scoreValue.text = finalScore.ToString();
    }

}
