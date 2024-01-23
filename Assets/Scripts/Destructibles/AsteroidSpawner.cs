using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour // Corrected capitalization
{
    // Reference to the asteroid prefab to be spawned
    public BaseDestructibleObject asteroidPrefab;

    // Variance in trajectory angle for spawned asteroids
    public float trajectoryVariance = 15.0f;

    // Rate at which asteroids are spawned (in seconds)
    public float spawnRate = 2.0f;

    // Distance from the spawner at which asteroids are spawned
    public float spawnDistance = 15.0f;

    // Number of asteroids to spawn in each wave
    public int spawnAmount = 1; // Corrected syntax for defining an interface member

    // Called when the script instance is being loaded
    private void Start()
    {
        // Invoke the Spawn method repeatedly based on the spawnRate
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    // Method to spawn asteroids
    private void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++) // Removed 'This' and corrected capitalization
        {
            // Generate a random direction within a unit circle and normalize it
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;

            // Calculate the spawn point based on the spawner's position and the spawn direction
            Vector3 spawnPoint = transform.position + spawnDirection;

            // Introduce some variance to the trajectory by rotating it
            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            // Instantiate an asteroid at the calculated spawn point with the rotated trajectory
            Instantiate(asteroidPrefab, spawnPoint, rotation);

        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position,spawnDistance);
    }
#endif

}
