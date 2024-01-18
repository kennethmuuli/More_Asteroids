using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseDestructibleObject
{
   void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.tag == "Asteroid")
        {
           Die();
        }
    }
}
