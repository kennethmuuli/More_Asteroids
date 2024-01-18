
using System;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Asteroid : BaseDestructibleObject
{
    // Points you get for destroying an asteroid 
    //[SerializeField] int pointValue;
    //Scoretracker scoretracker; 

    // Get scoretracker
    /*void Start()
    {
        scoretracker = FindFirstObjectByType<Scoretracker>();
    }*/

    // Destroy asteroid if it collides with projectile
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.tag == "Projectile")
        {
            // Add asteroidPoints to score if asteroid is destroyed
            // Must make UpdateScore() public and remove ScoreTracker script
            // from gameObject (Asteroid prefab) 
            //scoretracker.UpdateScore(pointValue);
            Die();
        }
    }
}