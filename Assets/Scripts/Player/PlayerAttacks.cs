using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    // Variable to store projectile prefab
    [SerializeField] private GameObject projectilePrefab;
    // Projectile flight speed 
    [SerializeField] float projectileSpeed = 10f;
    // The time after which the object is destroyed 1f = 1s
    [SerializeField] float projectileLifetime = 5f;
    // Firing interval
    [SerializeField] float firingRate = 0.2f;
    // A variable that determines whether a shot is currently being fired, by default is false
    private bool isFiring;
    // A routine that executes firing repeatedly
    Coroutine firingCoroutine;

    // This function is called when the object becomes enabled and active. 
    private void OnEnable() {
        // Subscribing to event
        PlayerController.firing += Fire;
    }

    // This function is called when the behaviour becomes disabled or inactive.
    private void OnDisable() {
        // Unsubscribing to event
        PlayerController.firing -= Fire;
    }
    
    //FixedUpdate is called every fixed frame-rate frame.
    void FixedUpdate()
    {
        Fire();
    }

    // Fire one or multiple projectiles
    void Fire()
    {
        // If space is pressed isFiring == true
        isFiring = Input.GetKey(KeyCode.Space);

        // if firing is true and firing coroutine is not assigned start shooting 
        if(isFiring && firingCoroutine == null)
        {
            // Start firing coroutine
            firingCoroutine = StartCoroutine(SpawnProjectile());
        }
        // if not firing and firing coroutine is still assigned stop shooting
        else if(!isFiring && firingCoroutine != null)
        {
            // Stop firing coroutine
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
        
    }

    // Method to spawn one projectile
    IEnumerator SpawnProjectile()
    {
        while(true)
        {
            // Instantiate new projectile
            GameObject instance = Instantiate(projectilePrefab, transform.position, transform.rotation);

            // Access projectile`s rigidbody2D component
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

            // If rigidBody exists
            if(rb != null)
            {
                // Determine the direction and speed of the projectile
                // transform.up = The green axis of the transform in world space
                rb.velocity = transform.up * projectileSpeed;
            }

            // Destroy the projectile after the specified time
            Destroy(instance, projectileLifetime);
            // By default, Unity resumes a coroutine on the frame after a yield statement. 
            //If you want to introduce a time delay, use WaitForSeconds
            yield return new WaitForSeconds(firingRate);
            //yield return new WaitForFixedUpdate();
        }
    }
}

