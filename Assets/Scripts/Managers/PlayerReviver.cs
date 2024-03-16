using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerReviver : PowerUpComponent
{
    public static Action<int> playerRevived;
    private Dictionary<int, GameObject> playerDict = new Dictionary<int, GameObject>();
    private int _joinedPlayerCount;
    private PlayerInputManager playerInputManager;
    
    protected override void OnEnable() {
        base.OnEnable();
        GameManager.OnPublishPlayer += LimitPlayerJoin;
    }
    protected override void OnDisable() {
        base.OnDisable();
        GameManager.OnPublishPlayer -= LimitPlayerJoin;
    }

    void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void LimitPlayerJoin(int playerID, GameObject player) {
        
        _joinedPlayerCount++;
        
        if(_joinedPlayerCount == 2) {
            playerInputManager.DisableJoining();
        }

        playerDict.Add(playerID,player);
    }

    protected override void OnPowerUpCollected(PowerUpType powerUpType, float duration, int instanceIDToCheck)
    {
        if (powerUpType == myPowerUpType)
        {
            powerUpEngaged = true;
            powerUpDuration = Time.time + duration;
            OnPlayerRevived(instanceIDToCheck);
        } return;

    }

    private void OnPlayerRevived(int playerID) {
        foreach (KeyValuePair<int, GameObject> entry in playerDict)
        {
            //return if id matches the id of triggering player, i.e. don't trigger revive on self
            if (playerID == entry.Key) {
                continue;
            }
            
            //check if isActive to avoid resetting an active player
            if(entry.Value.activeSelf == false) {
                GameManager.instance.UpdatePlayerCount();
                // entry.Value.transform.position = Vector2.zero;
                // entry.Value.transform.rotation = Quaternion.identity;
                entry.Value.SetActive(true);
                PowerUp.PowerUpCollected?.Invoke(PowerUpType.Health, 0f, entry.Key);
            }

        }
    }
}
