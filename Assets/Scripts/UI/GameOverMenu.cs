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
    [SerializeField] private GameObject inputWindow;
    private bool isCoopGame;
    // Start is called before the first frame update
    private void OnEnable() {
        GameManager.AnnounceCoopGame += OnAnnounceCoopGame;
        GameManager.OnUpdateGameState += ToggleGameOverMenu;
        Scoretracker.PublishScore += ShowFinalScore;
    }

    private void OnDisable() {
        GameManager.AnnounceCoopGame -= OnAnnounceCoopGame;
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

    private void OnAnnounceCoopGame() {
        isCoopGame = true;
    }

    private void ShowFinalScore(int finalScore) {
        scoreValue.text = finalScore.ToString();
        if (SaveLoadSystem.instance.CheckIfHighScore(finalScore))
        {
            HighscoreAchieved(finalScore);
        }
    }

    private void HighscoreAchieved(int finalScore) {
        inputWindow.SetActive(true);
        inputWindow.GetComponent<InputWindow>().highscoreToSubmit = finalScore;
        inputWindow.GetComponent<InputWindow>().isCoopGame = isCoopGame;

    }

}
