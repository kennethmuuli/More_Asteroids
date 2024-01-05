using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDestructibleObject : MonoBehaviour
{
    [SerializeField] protected int myScoreValue;
    public static event Action<int> objectDestroyed;

    protected virtual void Die(){
        // Invoke objectDestroyed event sending in myScoreValue for all listeners
        objectDestroyed?.Invoke(myScoreValue);

        // any other logic...

        // Destory this gameObject | should remain the last thing that's done, as the code won't run after this
        Destroy(gameObject);
    }
}
