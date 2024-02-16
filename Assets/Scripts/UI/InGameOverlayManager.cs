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
    [SerializeField] private GameObject laserPowerUpDisplay; 
    [SerializeField] private Slider laserPUSlider;
    [SerializeField] private float laserPURemainingDuration;
    
    private void OnEnable() {
        Scoretracker.scoreUpdated += OnScoreUpdated;
        PowerUp.powerUpCollected += OnPowerUpCollected;
    }

    private void OnDisable() {
        Scoretracker.scoreUpdated -= OnScoreUpdated;
        PowerUp.powerUpCollected -= OnPowerUpCollected;
    }

    private void Update() {
        if (Time.time < laserPURemainingDuration)
        {
            UpdatePowerUpDurationDisplayBar();  
            
        } else laserPowerUpDisplay.SetActive(false);
    }
    private void OnPowerUpCollected(PowerUpType type, float duration)
    {
        laserPowerUpDisplay.SetActive(true);
        laserPUSlider.value = 1;
        laserPURemainingDuration = Time.time + duration;
    }

    private void UpdatePowerUpDurationDisplayBar () {
        laserPUSlider.value = Mathf.Abs(1 - Time.time / laserPURemainingDuration);

        // 5 - x / 5 = 0.8 , 0.6
        // 5 / x = 5, 2.5
        // 5 / x = 1, 0.8
    }

    private void OnScoreUpdated(int scoreToDisplay)
    {
    // Or should it be in Update method?
        score.text = scoreToDisplay.ToString();
    }
}
