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
    
    //FixedUpdate is called every fixed frame-rate frame.
    void FixedUpdate()
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

        if (IsFiring())
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.up, rayLength, destructiblesLayer);
    
            foreach (var hit in hits)
            {
                hit.transform.gameObject.GetComponent<BaseDestructibleObject>().Die();
            }
        }

    }

    private void OnDrawGizmos() {
        if (IsFiring())
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.position, transform.up * rayLength);
        }
    }
}

