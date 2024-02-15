using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    private bool powerUpEngaged;
    private float powerUpDuration;
    
    // Variable to store projectile prefab
    [Header("Cannon attack")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireCooldown;
    private int nextCannonIndex = 0;
    private float nextTimeToFire;
    [Header("Laser attack")]
    [SerializeField] private float rayLength = 60f;
    [SerializeField] private LayerMask destructiblesLayer;
    [SerializeField] private GameObject laserGFX;
    [SerializeField] private GameObject[] cannonPositions;
    [SerializeField] private bool showRaycast;
    
    
    private void OnEnable() {
        PowerUp.powerUpCollected += OnPowerUpCollected;
    }
    private void OnDisable() {
        PowerUp.powerUpCollected -= OnPowerUpCollected;
    }
    
    //FixedUpdate is called every fixed frame-rate frame.
    void Update()
    {
        if (IsFiring() && powerUpEngaged)
        {
            FireRayWeapon();
        } else if (IsFiring()) {
            Fire();  
        } 

        laserGFX.SetActive(IsFiring() && powerUpEngaged);

        if(Time.time > powerUpDuration) {
            powerUpEngaged = false;
        }
    }

    private bool IsFiring() => Input.GetKey(KeyCode.Space);

    // Fire one or multiple projectiles
    void Fire()
    {
        if (Time.unscaledTime > nextTimeToFire)
        {
            //Alternate cannons from which to fire from
            nextCannonIndex++;

            if (nextCannonIndex == cannonPositions.Length)
            {
                nextCannonIndex = 0;
            }
            
            Vector2 nextFirePos = cannonPositions[nextCannonIndex].transform.position;
            
            // Instantiate new projectile
            Instantiate(projectilePrefab, nextFirePos, transform.rotation);

            nextTimeToFire = Time.unscaledTime + fireCooldown;
        }  
    }

    private void FireRayWeapon(){

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, rayLength, destructiblesLayer);

        if(hit == true) {
            laserGFX.GetComponent<RayController>().SetUpLinePositions(transform.position, hit.point);      
            hit.transform.gameObject.GetComponent<BaseDestructibleObject>().TakeDamage(1);
        } else {
            laserGFX.GetComponent<RayController>().SetUpLinePositions(transform.position, transform.position + transform.up * rayLength);
        }

    }

    private void OnPowerUpCollected (PowerUpType powerUpType, float duration) {
        if (powerUpType == PowerUpType.Laser)
        {
            powerUpEngaged = true;
            powerUpDuration = Time.time + duration;
        } return;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        if (IsFiring() && powerUpEngaged && showRaycast)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.position, transform.up * rayLength);
        }
    }  
#endif

}


