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
    private float laserPURemainingDuration;
    [Header("Shield PU Display Components")]
    [SerializeField] private GameObject shieldPUDisplay; 
    [SerializeField] private Slider shieldPUSlider;
    private float shieldPURemainingDuration;
    
    private void OnEnable() {
        Scoretracker.scoreUpdated += OnScoreUpdated;
        PowerUp.powerUpCollected += OnPowerUpCollected;
    }

    private void OnDisable() {
        Scoretracker.scoreUpdated -= OnScoreUpdated;
        PowerUp.powerUpCollected -= OnPowerUpCollected;
    }

    private void Update() {
        // FIXME: Dirty implementation here
        if (Time.time < laserPURemainingDuration)
        {
            UpdatePowerUpDurationDisplayBar();  
            
        } else laserPUDisplay.SetActive(false);
        
        if (Time.time < shieldPURemainingDuration)
        {
            UpdatePowerUpDurationDisplayBar2();  
            
        } else shieldPUDisplay.SetActive(false);
    }
    private void OnPowerUpCollected(PowerUpType type, float duration)
    {
        switch (type)
        {
            case PowerUpType.Laser:
                laserPUDisplay.SetActive(true);
                laserPURemainingDuration = Time.time + duration;
                break;
            case PowerUpType.Shield:
                shieldPUDisplay.SetActive(true);
                shieldPURemainingDuration = Time.time + duration;
                break;
            default:
            break;
        }
        
        
    }

    // FIXME: Dirty implementation here
    private void UpdatePowerUpDurationDisplayBar () {
        laserPUSlider.value = Mathf.Abs(1 - Time.time / laserPURemainingDuration);
    }
    // FIXME: Dirty implementation here
    private void UpdatePowerUpDurationDisplayBar2 () {
        shieldPUSlider.value = Mathf.Abs(1 - Time.time / shieldPURemainingDuration);
    }

    private void OnScoreUpdated(int scoreToDisplay)
    {
    // Or should it be in Update method?
        score.text = scoreToDisplay.ToString();
    }
}
