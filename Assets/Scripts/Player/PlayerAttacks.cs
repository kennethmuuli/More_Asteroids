using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    // Variable to store projectile prefab
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireCooldown;
    private float nextTimeToFire;
    [SerializeField] private float rayLength = 60f;
    [SerializeField] private LayerMask destructiblesLayer;
    [SerializeField] private GameObject laserGFX;
    
    private void Start() {

    }
    
    //FixedUpdate is called every fixed frame-rate frame.
    void Update()
    {
        // Fire();
        FireRayWeapon();
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

    private void FireRayWeapon(){

        laserGFX.SetActive(IsFiring());

        if (IsFiring())
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, rayLength, destructiblesLayer);
    
            if(hit == true) {
                laserGFX.GetComponent<RayController>().SetUpLinePositions(transform.position, hit.point);      
                hit.transform.gameObject.GetComponent<BaseDestructibleObject>().TakeDamage(1);
            } else {
                laserGFX.GetComponent<RayController>().SetUpLinePositions(transform.position, transform.position + transform.up * rayLength);
            }
        } 
    }

    private void OnDrawGizmos() {
        if (IsFiring())
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.position, transform.up * rayLength);
        }

            nextTimeToFire = Time.unscaledTime + fireCooldown;
        }  

    }
}

