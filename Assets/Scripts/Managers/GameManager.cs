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
    private int _currentPlayerCount = 0;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
        } else {Destroy(gameObject);}
    }

    // Update is called once per frame
    public void PublishPlayerID (int playerInstanceID) {
        _currentPlayerCount++;
        OnPublishPlayerID?.Invoke(playerInstanceID);
    }

}
