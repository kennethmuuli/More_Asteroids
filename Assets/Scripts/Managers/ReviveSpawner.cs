using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject revivePowerUp;
    [Header("Spawn time randomiser")]
    [SerializeField, Tooltip("Minimum time to spawn revive power up in seconds.")] private float spawnTime;
    [SerializeField, Tooltip("Number of second to add to respawn powerup spawn, each time a player is respawned.")] private float spawnTimeIncrement;
    private bool keepLooping, isCoopGame;
    private Coroutine currentLoop;
    private void OnEnable() {
        GameManager.AnnounceCoopGame += ToggleCoopGame;
        GameManager.OnPlayerDied += StartLooping;
        PlayerReviver.OnPlayerRevived += StopLooping;
    }
    private void OnDisable() {
        GameManager.AnnounceCoopGame -= ToggleCoopGame;
        GameManager.OnPlayerDied -= StartLooping;
        PlayerReviver.OnPlayerRevived -= StopLooping;
    }

    private void ToggleCoopGame(){
        isCoopGame = true;
    }
    
    private void StopLooping(){
        keepLooping = false;
        StopCoroutine(currentLoop);
        spawnTime = spawnTime + spawnTimeIncrement;
    }

    private void StartLooping(){
        if (isCoopGame)
        {
            keepLooping = true;
            currentLoop = StartCoroutine(SpawnRevivePowerUps());
        }
    }

    private IEnumerator SpawnRevivePowerUps() {
        yield return new WaitForSeconds(spawnTime);

        Instantiate(revivePowerUp,Vector2.zero,Quaternion.identity);
        
        if (keepLooping)
        {
            currentLoop = StartCoroutine(SpawnRevivePowerUps());
        }
    }
}
