using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseDestructibleObject
{
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.tag == "Asteroid")
        {
            Die();
        }
    }
}
