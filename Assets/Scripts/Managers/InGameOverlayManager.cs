using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameOverlayManager : MonoBehaviour
{

    // Reference to the Text component in the Canvas
    public TextMeshProUGUI score;
    
    private void OnEnable() {
        Scoretracker.scoreUpdated += OnScoreUpdated;
    }
    private void OnDisable() {
        Scoretracker.scoreUpdated -= OnScoreUpdated;
    }

    private void OnScoreUpdated(int scoreToDisplay)
    {
    // Or should it be in Update method?
        score.text = scoreToDisplay.ToString();
    }
}
