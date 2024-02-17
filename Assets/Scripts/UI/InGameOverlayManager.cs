using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameOverlayManager : MonoBehaviour
{

    // Reference to the Text component in the Canvas
    [SerializeField] private TextMeshProUGUI score;
    [Header("Laser PU Display Components")]
    [SerializeField] private GameObject laserPUDisplay; 
    [SerializeField] private Slider laserPUSlider;
    [Header("Shield PU Display Components")]
    [SerializeField] private GameObject shieldPUDisplay; 
    [SerializeField] private Slider shieldPUSlider;
    
    private void OnEnable() {
        Scoretracker.scoreUpdated += OnScoreUpdated;
        PowerUp.powerUpCollected += OnPowerUpCollected;
    }

    private void OnDisable() {
        Scoretracker.scoreUpdated -= OnScoreUpdated;
        PowerUp.powerUpCollected -= OnPowerUpCollected;
    }

    private void OnPowerUpCollected(PowerUpType type, float PUDuration)
    {
        switch (type)
        {
            case PowerUpType.Laser:
                StartCoroutine(UpdatePowerUpBar(PUDuration, laserPUSlider, laserPUDisplay));
                break;
            case PowerUpType.Shield:
                StartCoroutine(UpdatePowerUpBar(PUDuration, shieldPUSlider, shieldPUDisplay));
                break;
            default:
            break;
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

        powerupDisplay.SetActive(false);
        
    }

    private void OnScoreUpdated(int scoreToDisplay)
    {
        score.text = scoreToDisplay.ToString();
    }
}
