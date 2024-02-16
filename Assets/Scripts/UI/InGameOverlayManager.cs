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
        laserPURemainingDuration = Time.time + duration;
    }

    private void UpdatePowerUpDurationDisplayBar () {
        laserPUSlider.value = Mathf.Abs(1 - Time.time / laserPURemainingDuration);
    }

    private void OnScoreUpdated(int scoreToDisplay)
    {
    // Or should it be in Update method?
        score.text = scoreToDisplay.ToString();
    }
}
