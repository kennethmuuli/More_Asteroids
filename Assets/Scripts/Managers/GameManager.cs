using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Action<int> OnPublishPlayerID;
    public static Action<GameState> OnUpdateGameState;
    private static GameState currentGameState;
    private int _currentPlayerCount = 0;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
        } else {Destroy(gameObject);}
    }

    private void Start() {
        UpdateGameState(GameState.Play);
    }

    // Update is called once per frame
    public void PublishPlayerID (int playerInstanceID) {
        _currentPlayerCount++;
        OnPublishPlayerID?.Invoke(playerInstanceID);
    }

    public void PlayerDied() {
        _currentPlayerCount--;
        print(_currentPlayerCount);
        if (_currentPlayerCount == 0)
        {
            UpdateGameState(GameState.GameOver);
        }
    }

    public void UpdateGameState (GameState newState) {
        currentGameState = newState;

        switch (newState)
        {
            case GameState.Play:
                Time.timeScale = 1f;
            break;
            case GameState.Pause:
                Time.timeScale = 0f;
            break;
            case GameState.GameOver:
                Time.timeScale = 0f;
            break;
            default:
            break;
        }

        OnUpdateGameState?.Invoke(newState);
    }

    public GameState GetCurrentGameState {
        get {return currentGameState;}
    }
}

public enum GameState {
    Play,
    Pause,
    GameOver
}
