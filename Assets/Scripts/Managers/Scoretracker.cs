using UnityEngine;
using TMPro;

public class Scoretracker : MonoBehaviour
{
    // Reference to the Text component in the Canvas
    public TextMeshProUGUI score;
    [SerializeField] private int currentScore = 0;
    [SerializeField] private int scorePlusAtTick;
    [SerializeField] private float secondsBetweenTicks;
    private float nextTickTime;

    // This function is called when the object becomes enabled and active.
    private void OnEnable()
    {
        // Subscribing to event
        Asteroid.objectDestroyed += UpdateScore;
    }

    // This function is called when the behaviour becomes disabled or inactive.
    private void OnDisable()
    {
        // Unsubscribing to event
        Asteroid.objectDestroyed -= UpdateScore;
    }

    // Update is called every frame, if the MonoBehaviour is enabled.
    private void Update()
    {
        TimeScoreTicks();
    }

    private void UpdateScore(int scoreToAdd)
    {
        currentScore = currentScore + scoreToAdd;
        // Or should it be in Update method?
        score.text = currentScore.ToString();
    }

    // Used to track and set time ticks for updating the score based on how long the player has been alive for
    private void TimeScoreTicks()
    {
        // Comparing if current time is later than nextTickTime, if yes, set nextTickTime and call UpdateScore
        if (Time.unscaledTime > nextTickTime)
        {
            nextTickTime = Time.unscaledTime + secondsBetweenTicks;
            UpdateScore(scorePlusAtTick);
        }
    }
}
