using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private int damageAmount = 1;
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
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Asteroid")
        {
            other.transform.gameObject.GetComponent<BaseDestructibleObject>().TakeDamage(damageAmount);
            Destroy(gameObject);
        } else if (other.tag == "Megaroid") {
            other.transform.gameObject.GetComponentInParent<MegaAsteroid>().TakeDamage(damageAmount);
            Destroy(gameObject);
        } else if (other.tag == "Meteorite" || other.tag == "MegaroidShield") {
            Destroy(gameObject);
        }
    }
    
}
