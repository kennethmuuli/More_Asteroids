using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputWindow : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject buttonToHide,messageToShow;
    [SerializeField] private TextMeshProUGUI highscoreMessage;

    [HideInInspector] public int highscoreToSubmit;
    [HideInInspector] public bool isCoopGame;

    private void Start()
    {
        if (isCoopGame)
        {
            highscoreMessage.text = "new coop highscore earned";
        } else {
            highscoreMessage.text = "new solo highscore earned";
        }
        buttonToHide.SetActive(false);
        messageToShow.SetActive(true);
        
        inputField.characterLimit = 25;
        inputField.contentType = TMP_InputField.ContentType.Standard;
        inputField.lineType = TMP_InputField.LineType.SingleLine;

        // Ensure input field is focused
        if (!inputField.isFocused)
        {
            inputField.Select();
            inputField.ActivateInputField();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnSubmit();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClear();
        }
    }

    public void OnSubmit()
    {
        string highscoreOwnerName = inputField.text;

        if (string.IsNullOrEmpty(highscoreOwnerName.Trim()))
        {
            Debug.LogWarning("No name for highscore set, please enter a name");
            inputField.ActivateInputField();
            return;
        }

        SaveLoadSystem.instance.InsertHighScore(highscoreOwnerName, highscoreToSubmit, isCoopGame);
        SceneManager.LoadScene(0);
    }

    public void OnClear()
    {
        inputField.text = "";
    }

}
