using System;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectToDestory : BaseDestructibleObject
{

    // Start is called before the first frame update
    void Start()
    {
        /* Inherited from abstract parent class, use wherever if you don't want to change the implementation OR if you have changed the base implementation, this will call out the implementation from this class */
        Die();
        /* Alternatively, even after changing the base implementation (see override below), you can call out the base implementation with the following line */
        // base.Die();
    }

    /* Use the code below if you want to add to (override) the base implementation, adding you logic before the base implementation is called out. TODO: >>>REMOVE IF UNUSED<<< */
    /*

        protected override void Die()
        {
            // any other logic... (example on following line)
            myScoreValue = myScoreValue * 2;

            base.Die();
        }

    */
}
