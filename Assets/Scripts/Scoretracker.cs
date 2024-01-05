using UnityEngine;

public class Scoretracker : MonoBehaviour
{
    [SerializeField] private int currentScore = 0;

    // This function is called when the object becomes enabled and active.
    private void OnEnable() {
        // Subscribing to event
        ObjectToDestory.objectDestroyed += UpdateScore;
    }

    // This function is called when the behaviour becomes disabled or inactive.
    private void OnDisable() {
        // Unsubscribing to event
        ObjectToDestory.objectDestroyed += UpdateScore;
    }

    private void UpdateScore(int scoreToAdd)
    {
        currentScore = currentScore + scoreToAdd;
    }
}
