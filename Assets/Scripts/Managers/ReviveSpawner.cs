using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject revivePowerUp;
    [Header("Spawn time randomiser")]
    [SerializeField, Tooltip("Minimum time to spawn revive power up in seconds.")] private float minSpawnTime;
    [SerializeField, Tooltip("Maximum time to spawn revive power up in seconds.")] private float maxSpawnTime;
    private bool keepLooping, isCoopGame;
    private Coroutine currentLoop;
    private void OnEnable() {
        GameManager.AnnounceCoopGame += ToggleCoopGame;
        GameManager.OnPlayerDied += StartLooping;
        PlayerReviver.OnPlayerRevived += StopLooping;

        if(minSpawnTime > maxSpawnTime) {
            maxSpawnTime = minSpawnTime;
        }
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
    }

    private void StartLooping(){
        if (isCoopGame)
        {
            keepLooping = true;
            currentLoop = StartCoroutine(SpawnRevivePowerUps());
        }
    }

    private IEnumerator SpawnRevivePowerUps() {
        yield return new WaitForSeconds(Random.Range(minSpawnTime,maxSpawnTime));

        Instantiate(revivePowerUp,Vector2.zero,Quaternion.identity);
        
        if (keepLooping)
        {
            currentLoop = StartCoroutine(SpawnRevivePowerUps());
        }
    }
}
