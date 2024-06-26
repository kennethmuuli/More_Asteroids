using System.Collections;
using UnityEngine;

public class DestructibleSpawner : MonoBehaviour
{

#region Variables
    [Header("General")]
    [SerializeField, Tooltip("Reference to the prefab to be spawned")] private GameObject[] spawnPrefabs;
    [SerializeField, Tooltip("Radius from the transform at which objects are spawned")] private float spawnCircleRadius = 15.0f;
    [SerializeField, Tooltip("Radius from the transform at which the destination point for a spawned object is calculated")] private float destCircleRadius = 3f;

    [Header("Asteroid spawning")]
    [SerializeField, Tooltip("Amount in seconds before these objects start spawning")] private int asteroidStartDelay;
    [SerializeField, Tooltip("Number of objects to spawn in with each spawn wave")] private int asteroidAmount;
    [SerializeField, Tooltip("Rate at which objects are spawned (in seconds)")] private float asteroidRate;
    [SerializeField, Tooltip("Set which x difficulty update call to respond to, a call is made every 10 seconds, e.g. 6 = 1 minute"), Range(1,600)] private int asteroidCallStep = 1;
    [SerializeField, Tooltip("Amount of objects to add to relevant spawn wave when difficultyStep happens")] private int asteroidSpawnInc;
    [Header("Meteorite spawning")]
    [SerializeField, Tooltip("Amount in seconds before these objects start spawning")] private int meteoriteStartDelay;
    [SerializeField, Tooltip("Number of objects to spawn in with each spawn wave")] private int meteoriteAmount;
    [SerializeField, Tooltip("Rate at which objects are spawned (in seconds)")] private float meteoriteRate;
    [SerializeField, Tooltip("Set which x difficulty update call to respond to, a call is made every 10 seconds, e.g. 6 = 1 minute"), Range(1,600)] private int meteoriteCallStep = 1;
    [SerializeField, Tooltip("Amount of objects to add to relevant spawn wave when difficultyStep happens")] private int meteoriteSpawnInc;
    [Header("Megaroid spawning")]
    [SerializeField, Tooltip("Amount in seconds before these objects start spawning")] private int megaroidStartDelay;
    [SerializeField, Tooltip("Number of objects to spawn in with each spawn wave")] private int megaroidAmount;
    [SerializeField, Tooltip("Rate at which objects are spawned (in seconds)")] private float megaroidRate;
    [SerializeField, Tooltip("Set which x difficulty update call to respond to, a call is made every 10 seconds, e.g. 6 = 1 minute"), Range(1,600)] private int megaroidCallStep = 1;
    [SerializeField, Tooltip("Amount of objects to add to relevant spawn wave when difficultyStep happens")] private int megaroidSpawnInc;
    [SerializeField, Tooltip("How many of this object can be spawned in at any given time?")] private int megaroidLimitInScene;
    private int currentMegaroidsInScene;
    

    // Reference to main camera
    private Camera mainCamera;
    #endregion

    private void OnEnable() {
        GameManager.IncreaseDifficulty += OnIncreaseDifficulty;
        MegaAsteroid.MegaroidDestroyed += OnMegaroidDestroyed;
    }
    private void OnDisable() {
        GameManager.IncreaseDifficulty -= OnIncreaseDifficulty;
        MegaAsteroid.MegaroidDestroyed -= OnMegaroidDestroyed;
    }

    private void Start()
    {
        CenterPosMainCameraXY();

        currentMegaroidsInScene = megaroidAmount;

        //StartCoroutineLoopsForEachObject
        StartCoroutine(Spawn(asteroidRate,asteroidAmount,spawnPrefabs[0], asteroidStartDelay));
        StartCoroutine(Spawn(meteoriteRate,meteoriteAmount,spawnPrefabs[1], meteoriteStartDelay));
        StartCoroutine(Spawn(megaroidRate,megaroidAmount,spawnPrefabs[2], megaroidStartDelay));
    }

    private void CenterPosMainCameraXY(){
        mainCamera = Camera.main;
        transform.position = new Vector2(mainCamera.transform.position.x,mainCamera.transform.position.y);
    }

    // Method to spawn asteroids
    private IEnumerator Spawn(float spawnRate, int amountOfObjectsToSpawn, GameObject objectToSpawn, int startDelay = 0)
    {
        //Used only for initial loop to delay when the objects start spawning
        if (startDelay != 0)
        {
            yield return new WaitForSeconds(startDelay);
        }

        yield return new WaitForSeconds(spawnRate);
        if (amountOfObjectsToSpawn != 0)
        {
            for (int i = 0; i < amountOfObjectsToSpawn; i++)
            {
                var values = CalculateSpawnPointAndDestAngle();
    
                // Instantiate an asteroid at the calculated spawn point with the rotated trajectory
                Instantiate(objectToSpawn, values.spawnPoint, Quaternion.Euler(0,0,values.angle),transform);
    
            }
        }

        yield return null;

        //Read in current variable values
        if(objectToSpawn == spawnPrefabs[0]){
            spawnRate = asteroidRate;
            amountOfObjectsToSpawn = asteroidAmount;
        }else if(objectToSpawn == spawnPrefabs[1]){
            spawnRate = meteoriteRate;
            amountOfObjectsToSpawn = meteoriteAmount;
        }else if(objectToSpawn == spawnPrefabs[2]){
            
            //The implementation is nasty, but it does the job and limits active megaroids that can be instantiated at any given time.
            if ((currentMegaroidsInScene + megaroidAmount) > megaroidLimitInScene)
            {
                amountOfObjectsToSpawn = megaroidLimitInScene - currentMegaroidsInScene;
                        
            } else amountOfObjectsToSpawn = megaroidAmount;

            spawnRate = megaroidRate;
            currentMegaroidsInScene = currentMegaroidsInScene + amountOfObjectsToSpawn;
        }

        StartCoroutine(Spawn(spawnRate, amountOfObjectsToSpawn, objectToSpawn));
    }

    private (Vector2 spawnPoint, float angle) CalculateSpawnPointAndDestAngle() {
        // Calculate spawnPoint on a circle's circumference
            Vector2 spawnPoint = Random.insideUnitCircle.normalized * spawnCircleRadius + (Vector2)transform.position;

        /* 
            Calculate a random point on a smaller circle's cricumference (inside first circle) to be used as 
            the target destination for the spawned object. This will create variance in it's trajectory. 
        */
        Vector2 destPoint = Random.insideUnitCircle.normalized * destCircleRadius + (Vector2)transform.position;

        // Calculate the angle between up direction and direction towards center
        float angle = Vector2.SignedAngle(Vector2.up, destPoint - spawnPoint);

        return (spawnPoint, angle);
    }

    private void OnIncreaseDifficulty(int difficultyCallNum){
        //Asteroids
        if (RespondToCall.ShouldRespondToCall(asteroidCallStep, difficultyCallNum) && Time.time > asteroidStartDelay)
        {
            asteroidAmount += asteroidSpawnInc;
        }

        //Meteorites
        if (RespondToCall.ShouldRespondToCall(meteoriteCallStep, difficultyCallNum) && Time.time > meteoriteStartDelay)
        {
            meteoriteAmount += meteoriteSpawnInc;
        }

        //MegaAsteroids
        if (RespondToCall.ShouldRespondToCall(megaroidCallStep, difficultyCallNum) && Time.time > megaroidStartDelay)
        {
            megaroidAmount += megaroidSpawnInc;
        }
    }

    private void OnMegaroidDestroyed(){
        currentMegaroidsInScene--;
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
