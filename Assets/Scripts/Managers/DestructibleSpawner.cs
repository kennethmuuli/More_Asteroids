using UnityEngine;

public class DestructibleSpawner : MonoBehaviour
{

#region Variables
    [SerializeField, Tooltip("Reference to the prefab to be spawned")] private GameObject[] spawnPrefabs;

    private GameObject nextObjectToSpawn;

    [SerializeField, Tooltip("Number of objects to spawn in with each spawn")] private int spawnAmount = 1;
    [SerializeField, Tooltip("Rate at which asteroids are spawned (in seconds)")] private float spawnRate = 2.0f;
    [SerializeField, Tooltip("Radius from the transform at which objects are spawned")] private float spawnCircleRadius = 15.0f;
    [SerializeField, Tooltip("Radius from the transform at which the destination point for a spawned object is calculated")] private float destCircleRadius = 3f;

    // Reference to main camera
    private Camera mainCamera;
    [Header("Difficulty scaling")]
    [SerializeField, Tooltip("Time in seconds between spawn amount increases, i.e. how many objects are spawned at each spawn")] private float timeToIncreaseSpawnAmount;
    private float nextTimeToIncreaseSpawnAmount;
    [SerializeField] private float timeToIncreaseSpawnRate;
    [SerializeField] private float spawnRateDecreaseIncrement = 0.05f;
    [SerializeField] private float minSpawnRate = 0.75f;
    private float nextTimeToIncreaseSpawnRate;
    #endregion

    // Called when the script instance is being loaded




    private void Start()
    {
        CenterPosMainCameraXY();
        
        // Invoke the Spawn method repeatedly based on the spawnRate
        InvokeRepeating(nameof(Spawn), 0f, spawnRate);

        nextObjectToSpawn = spawnPrefabs[0];
        nextTimeToIncreaseSpawnAmount = Time.time + timeToIncreaseSpawnAmount;
        nextTimeToIncreaseSpawnRate = Time.time + timeToIncreaseSpawnRate;
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
            Instantiate(nextObjectToSpawn, spawnPoint, Quaternion.Euler(0,0,angle),transform);

            ChooseNextObjectToSpawn();
            ScaleDifficulty();
        }
    }

    private void ChooseNextObjectToSpawn() {

        float spawnRoll = Random.Range(0,100);
     
        if (spawnRoll <= 90) // Asteroid
        {
            nextObjectToSpawn = spawnPrefabs[0];
        } else if (spawnRoll <= 99) // Meteorite
        {
            nextObjectToSpawn = spawnPrefabs[1];
        } else nextObjectToSpawn = spawnPrefabs[2]; // MegaAsteroid
    }

    private void ScaleDifficulty(){
        if (Time.time > nextTimeToIncreaseSpawnAmount)
        {
            spawnAmount++;
            nextTimeToIncreaseSpawnAmount = Time.time + timeToIncreaseSpawnAmount;
        }

        if (spawnRate <= minSpawnRate)
        {
            spawnRate = minSpawnRate;
            return;
        } else {
            if(Time.time > nextTimeToIncreaseSpawnRate) {
                spawnRate = spawnRate - spawnRateDecreaseIncrement;
                nextTimeToIncreaseSpawnRate = Time.time + timeToIncreaseSpawnRate;
            
            }
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
