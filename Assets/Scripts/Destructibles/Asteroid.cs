
using System;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Asteroid : BaseDestructibleObject
{
    // Destroy asteroid if it collides with projectile
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.tag == "Projectile")
        {
            Die();
        }
    }
}