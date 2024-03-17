using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Action<int, GameObject> OnPublishPlayer;
    public static Action<GameState> OnUpdateGameState;
    private static GameState currentGameState;
    private int _currentPlayerCount = 0;
    private bool isCoopGame;
    public static Action AnnounceCoopGame;
    // How many difficulty increase calls have been made
    private int difficultyCallNum;
    private float timeToIncreaseDifficulty;
    public static Action<int> IncreaseDifficulty;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
        } else {Destroy(gameObject);}
    }

    private void Start() {
        UpdateGameState(GameState.Play);
        InvokeRepeating(nameof(ScaleDifficulty),10,10);
    }

    // Update is called once per frame
    public void PublishPlayerID (int playerInstanceID, GameObject player) {
        _currentPlayerCount++;
        OnPublishPlayer?.Invoke(playerInstanceID, player);

        if(_currentPlayerCount > 1 && !isCoopGame) {
            isCoopGame = true;
            AnnounceCoopGame?.Invoke();
        }
    }

    public void ScaleDifficulty(){
            difficultyCallNum++;
            IncreaseDifficulty?.Invoke(difficultyCallNum);
    }

    public void UpdatePlayerCount() {
        _currentPlayerCount++;
    }

    public void PlayerDied() {
        _currentPlayerCount--;
        if (_currentPlayerCount == 0)
        {
            UpdateGameState(GameState.GameOver);
        }
    }

    public void UpdateGameState (GameState newState) {
        currentGameState = newState;

        switch (newState)
        {
            case GameState.Keybind:
                Time.timeScale = 0f;
            break;
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
    Keybind,
    Play,
    Pause,
    GameOver
}
