using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    public float spawnCircleRadius = 15.0f;
    // Distance from the transform at which the destination point for a spawned object is calculated
    [SerializeField] float destCircleRadius = 3f;

    // Number of asteroids to spawn in each wave
    public int spawnAmount = 1; // Corrected syntax for defining an interface member
    // Reference to main camera
    private Camera mainCamera;

    // Called when the script instance is being loaded
    private void Start()
    {
        CenterPosMainCameraXY();
        
        // Invoke the Spawn method repeatedly based on the spawnRate
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void CenterPosMainCameraXY(){
        mainCamera = Camera.main;
        transform.position = new Vector2(mainCamera.transform.position.x,mainCamera.transform.position.y);
    }

    // Method to spawn asteroids
    private void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++) // Removed 'This' and corrected capitalization
        {
            // Calculate spawnPoint on a circle's circumference
            Vector2 spawnPoint = Random.insideUnitCircle.normalized * spawnCircleRadius + (Vector2)transform.position;

            /* 
                Calculate a random point on a smaller circle's cricumference (inside first circle) to be used as 
                the target destination for the spawned object. This will create variance in it's trajectory. 
            */
            Vector2 destPoint = Random.insideUnitCircle.normalized * destCircleRadius + (Vector2)transform.position;

            // Calculate the angle between up direction and direction towards center
            float angle = Vector2.SignedAngle(Vector2.up, destPoint - spawnPoint);

            // Instantiate an asteroid at the calculated spawn point with the rotated trajectory
            Instantiate(asteroidPrefab, spawnPoint, Quaternion.Euler(0,0,angle));
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position,spawnCircleRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,destCircleRadius);
    }
#endif

}
