using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDestructibleObject : MonoBehaviour
{
    [SerializeField] protected int myScoreValue;
    protected bool iGotHit = false;
    public static event Action<int> objectDestroyed;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            iGotHit = true;
        }
    }

    protected virtual void Die()
    {
        // Invoke objectDestroyed event sending in myScoreValue for all listeners
        objectDestroyed?.Invoke(myScoreValue);

        // any other logic...

        // Destroy this gameObject | should remain the last thing that's done, as the code won't run after this
        Destroy(gameObject);
    }
}
