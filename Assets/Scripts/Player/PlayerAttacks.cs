using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    // Variable to store projectile prefab
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireCooldown;
    private float nextTimeToFire;
    
    //FixedUpdate is called every fixed frame-rate frame.
    void FixedUpdate()
    {
        Fire();
    }

    private bool IsFiring() => Input.GetKey(KeyCode.Space);

    // Fire one or multiple projectiles
    void Fire()
    {
        if (IsFiring() && Time.unscaledTime > nextTimeToFire)
        {
            // Instantiate new projectile
            Instantiate(projectilePrefab, transform.position, transform.rotation);

            nextTimeToFire = Time.unscaledTime + fireCooldown;
        }  
    }
}

