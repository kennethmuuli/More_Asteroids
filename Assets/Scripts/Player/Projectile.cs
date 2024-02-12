using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Projectile flight speed 
    [SerializeField] float projectileSpeed = 10f;
    // The time after which the object is destroyed 1f = 1s
    [SerializeField] float projectileLifetime = 5f;

    private void Start() {
        // Access projectile`s rigidbody2D component
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            rb.velocity = transform.up * projectileSpeed;

            Destroy(gameObject,projectileLifetime);
    }
    
    // Destroy projectile if it collides with asteroid
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.tag == "Asteroid")
        {
            Destroy(gameObject);
        }
    }
}
