using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerReviver : MonoBehaviour
{
    public static Action<int> playerRevived;
    private Dictionary<int, GameObject> playerDict = new Dictionary<int, GameObject>();
    private int _joinedPlayerCount;
    private PlayerInputManager playerInputManager;
    // Start is called before the first frame update
    private int secondPlayerID;
    
    private void OnEnable() {
        GameManager.OnPublishPlayer += LimitPlayerJoin;
    }
    private void OnDisable() {
        GameManager.OnPublishPlayer -= LimitPlayerJoin;
    }

    void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void LimitPlayerJoin(int playerID, GameObject player) {
        
        _joinedPlayerCount++;
        
        if(_joinedPlayerCount == 2) {
            secondPlayerID = playerID;
            playerInputManager.DisableJoining();
        }

        playerDict.Add(playerID,player);
    }

    private void OnPlayerRevived(int playerID) {
        
        foreach (KeyValuePair<int, GameObject> entry in playerDict)
        {
            if (playerID != entry.Key) {
                continue;
            }
            
            GameManager.instance.UpdatePlayerCount();
            entry.Value.transform.position = Vector2.zero;
            entry.Value.transform.rotation = Quaternion.identity;
            entry.Value.SetActive(true);

        }
    }
}
