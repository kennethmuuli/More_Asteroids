using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameOverlayManager : MonoBehaviour
{

    // Reference to the Text component in the Canvas
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject inGameOverlay;
    [Header("Player One Components")]
    [SerializeField] private TextMeshProUGUI healthText1;
    [SerializeField] private GameObject laserPUDisplay1; 
    [SerializeField] private Slider laserPUSlider1;
    [SerializeField] private GameObject shieldPUDisplay1; 
    [SerializeField] private Slider shieldPUSlider1;
    [SerializeField] private GameObject speedPUDisplay1; 
    [SerializeField] private Slider speedPUSlider1;
    [Header("Player Two Components")]
    [SerializeField] private GameObject playerHealth2Display;
    [SerializeField] private TextMeshProUGUI healthText2;
    [SerializeField] private GameObject laserPUDisplay2; 
    [SerializeField] private Slider laserPUSlider2;
    [SerializeField] private GameObject shieldPUDisplay2; 
    [SerializeField] private Slider shieldPUSlider2;
    [SerializeField] private GameObject speedPUDisplay2; 
    [SerializeField] private Slider speedPUSlider2;
    private int playerOneID;
    private int playerTwoID;
    // Set up a nested dictionary, where the first level maps playerId to second level which maps powerUpTypeToCoroutine
    private Dictionary<int, Dictionary<PowerUpType, Coroutine>> activeCoroutines = new Dictionary<int, Dictionary<PowerUpType, Coroutine>>();
    
    private void OnEnable() {
        Scoretracker.ScoreUpdated += OnScoreUpdated;
        PowerUp.PowerUpCollected += OnPowerUpCollected;
        GameManager.OnPublishPlayer += AssignOverlaySide;
        GameManager.OnUpdateGameState += ToggleInGameOverlay;
        PlayerHull.OnPlayerHealthUpdated += OnPlayerHealthUpdated;
    }

    private void OnDisable() {
        Scoretracker.ScoreUpdated -= OnScoreUpdated;
        PowerUp.PowerUpCollected -= OnPowerUpCollected;
        GameManager.OnPublishPlayer -= AssignOverlaySide;
        GameManager.OnUpdateGameState -= ToggleInGameOverlay;
        PlayerHull.OnPlayerHealthUpdated += OnPlayerHealthUpdated;
    }

    private void OnPlayerHealthUpdated(int instanceIDToCheck, int newHealth)
    {
        if (instanceIDToCheck == playerOneID)
        {
            healthText1.text = newHealth.ToString();
        } else if (instanceIDToCheck == playerTwoID) {
            healthText2.text = newHealth.ToString();
        }
    }


    private void ToggleInGameOverlay(GameState stateToCheck) {
        if (stateToCheck == GameState.Pause || stateToCheck == GameState.GameOver)
        {
            inGameOverlay.GetComponent<Canvas>().enabled = false;
        } else if (stateToCheck == GameState.Play) {
            inGameOverlay.GetComponent<Canvas>().enabled = true;
        } 
    }

    private void AssignOverlaySide(int playerIndex, GameObject player) {
        _ = player;
        
        if (playerOneID == 0)
        {
            playerOneID = playerIndex;
        } else {
            playerTwoID = playerIndex;
            playerHealth2Display.SetActive(true);
        }
    }

    private void OnPowerUpCollected(PowerUpType type, float PUDuration, int instanceIDToCheck)
    {

        var components = SelectOverlayComponents(type, instanceIDToCheck); 

        if (components.slider == null || components.display == null) {
            return;
        }
        
        // check which player the passed in ID corresponds to
        int playerID = instanceIDToCheck == playerOneID ? playerOneID : playerTwoID;

        // check if a player has any coroutines running, if not start a new dict for them
        if (!activeCoroutines.ContainsKey(playerID))
        {
            activeCoroutines[playerID] = new Dictionary<PowerUpType, Coroutine>();
        }

        // check if a player already has a coroutine for a certain type of power up running, if so, stop said coroutine
        if (activeCoroutines[playerID].ContainsKey(type))
        {
            StopCoroutine(activeCoroutines[playerID][type]);
        }

        // start a new coroutine and register it's id and type in the activeCoroutine dictionary
        activeCoroutines[playerID][type] = StartCoroutine(UpdatePowerUpBar(PUDuration, components.slider, components.display));
    }

    private (Slider slider, GameObject display) SelectOverlayComponents(PowerUpType type, int instanceIDToCheck) {
    Slider selectedSlider;
    GameObject selectedGameObject;
        
        switch (type)
        {
            case PowerUpType.Laser:
                if (instanceIDToCheck == playerOneID)
                {
                    selectedSlider = laserPUSlider1;
                    selectedGameObject = laserPUDisplay1;
                } else {
                    selectedSlider = laserPUSlider2;
                    selectedGameObject = laserPUDisplay2;
                }
                return (selectedSlider, selectedGameObject);
            case PowerUpType.Shield:
                if (instanceIDToCheck == playerOneID)
                {
                    selectedSlider = shieldPUSlider1;
                    selectedGameObject = shieldPUDisplay1;
                } else {
                    selectedSlider = shieldPUSlider2;
                    selectedGameObject = shieldPUDisplay2;
                }
                return (selectedSlider, selectedGameObject);
            case PowerUpType.Speed:
                if (instanceIDToCheck == playerOneID)
                {
                    selectedSlider = speedPUSlider1;
                    selectedGameObject = speedPUDisplay1;
                } else {
                    selectedSlider = speedPUSlider2;
                    selectedGameObject = speedPUDisplay2;
                }
                return (selectedSlider, selectedGameObject);
            case PowerUpType.Health:
                return (selectedSlider = null, selectedGameObject = null);
            case PowerUpType.Revive:
                return (selectedSlider = null, selectedGameObject = null);
            default:
            Debug.LogError("No matching overlay components found.");
            return (selectedSlider = null, selectedGameObject = null);
        }
    }

    IEnumerator UpdatePowerUpBar (float powerupDuration, Slider powerupSlider, GameObject powerupDisplay) {
        powerupDisplay.SetActive(true);
        
        float timeElapsed = 0;

        while (timeElapsed < powerupDuration){
            float t = timeElapsed / powerupDuration;
            powerupSlider.value = Mathf.Lerp(1,0,t);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        powerupSlider.value = 0;
        AudioManager.PlaySFX?.Invoke(SFXName.PowerUpDisengage,gameObject.GetInstanceID());
        powerupDisplay.SetActive(false); 
    }

    private void OnScoreUpdated(int scoreToDisplay)
    {
        scoreText.text = scoreToDisplay.ToString();
    }
}
