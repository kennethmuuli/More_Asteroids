using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour // Corrected capitalization
{
    // Reference to the asteroid prefab to be spawned
    public Asteroid asteroidPrefab;

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
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    // Method to spawn asteroids
    private void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++) // Removed 'This' and corrected capitalization
        {
            // Generate a random direction within a unit circle and normalize it
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;

            // Calculate the spawn point based on the spawner's position and the spawn direction
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            // Introduce some variance to the trajectory by rotating it
            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            // Instantiate an asteroid at the calculated spawn point with the rotated trajectory
            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);

            // Set the size of the asteroid within the specified range
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);

            // Set the trajectory of the asteroid based on the rotated spawn direction
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }

    // Update is called once per frame
}
