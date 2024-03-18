using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class BaseDestructibleObject : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] protected int myScoreValue;
    [Header("Size")]
    [SerializeField] protected bool randomizeSizeOn;
    [SerializeField, Range(0.5f, 1f)] protected float minSize = 0.5f;
    [SerializeField, Range(1f, 3f)] protected float maxSize = 1.5f;
    [Header("Speed")]
    [SerializeField, Range(0f, 200f)] protected float speed = 50.0f;
    [SerializeField, Tooltip("+- how much the objects speed may vary, with each instantation"),Range(0f,50f)] protected float speedVariance = 0f;
    [Header("Health")]
    [SerializeField] protected int health = 1;
    protected int currentHealth;
    [SerializeField, Tooltip("Time in seconds the object is invulnerable between each damage application."), Range(0f,0.2f)] protected float invulnerabilityTime = 0f;
    protected bool isInvulnerable;
    [Header("Pickup Drops")]
    [SerializeField] private bool dropsPowerUps = false;
    [SerializeField] protected GameObject[] powerUpsDropList;
    
    [Range(0,100)]
    [SerializeField] protected float dropChance;
    [Tooltip("How likely, in percentages the destruction of this object is will drop something from the power ups drop list.")]
    protected Rigidbody2D _rigidbody;

    protected Renderer objectRenderer;
    private bool hasBeenInView;
    public static event Action<int> objectDestroyed;

    protected virtual void Awake()
    {
        // Get the Rigidbody2D component attached to this GameObject
        _rigidbody = GetComponent<Rigidbody2D>();
        objectRenderer = GetComponentInChildren<Renderer>();
    }
    protected virtual void Start() {
        currentHealth = health;
    }
    protected virtual void Update() {
        OffScreenBehaviour();
    }


    protected void RandomizeSize()
    {
        if (randomizeSizeOn)
        {
            // Get a random size between the max and min size
            float size = UnityEngine.Random.Range(minSize, maxSize);
    
            // Set the scale of the asteroid based on the 'size' variable
            transform.localScale = Vector2.one * size;
        }
    }

    protected void MoveAndSpin(Vector2 direction, float torque = 2f)
    {
        // Randomize start speed
        float speedToAdd = Random.Range(speed - speedVariance, speed + speedVariance);
        // Add a force to the Rigidbody2D to set the asteroid in motion
        _rigidbody.AddForce(direction * speedToAdd);
        // Add a spin force to the object, if not set then default value is used
        _rigidbody.AddTorque(torque);
    }

    protected void OffScreenBehaviour()
    {
        // Check whether the renderer is visible and if it hasn't been visible before, set hasBeenInView true
        if (objectRenderer.isVisible && !hasBeenInView)
        {
            hasBeenInView = true;
        }

        // Check whether the renderer is invisible and if it has been visible before, destroy gameObject
        if (!objectRenderer.isVisible && hasBeenInView)
        {
            Destroy(gameObject);
        }
    }

    // This method serves as the external input for this class
    public virtual void TakeDamage(int damageAmount){
       // Avoid taking damage before appearing on screen
       if (hasBeenInView && !isInvulnerable)
       {
           currentHealth -= damageAmount;
           StartCoroutine(InvulnerableTime(invulnerabilityTime));
       }
    }

    private IEnumerator InvulnerableTime(float timeToKeepInvulnerable) {
        isInvulnerable = true;
        yield return new WaitForSeconds(timeToKeepInvulnerable);
        isInvulnerable = false;

    }

    protected void Die(float destroyDelay = 0)
    {
        // Invoke objectDestroyed event sending in myScoreValue for all listeners
        objectDestroyed?.Invoke(myScoreValue);

        DropPowerUp();

        // Destroy this gameObject | should remain the last thing that's done, as the code won't run after this
        Destroy(gameObject, destroyDelay);
    }

    protected void DropPowerUp() {
        if (dropsPowerUps)
        {
            float dropRoll = Random.Range(0,100);
    
            if (dropChance > dropRoll)
            {
                int randomPUIndex = Random.Range(0,powerUpsDropList.Length);
                Instantiate(powerUpsDropList[randomPUIndex],transform.position,Quaternion.identity);
            }
        } else return;
    }
}
