
using System;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Asteroid : BaseDestructibleObject
{
    private void FixedUpdate() 
    {
        CheckForCollisions();
    }

    private void CheckForCollisions()
    {
        if (iGotHit)
        {
            Die();
        }
    }
}