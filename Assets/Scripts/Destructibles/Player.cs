using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseDestructibleObject
{
    // Destroy player if it collides with asteroid
   void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.tag == "Asteroid")
        {
           Die();
        }
    }
}
