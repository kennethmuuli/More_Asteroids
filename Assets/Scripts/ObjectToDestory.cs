using System;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;


public class ObjectToDestroy : BaseDestructibleObject
{
    protected bool iGotHit = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet") {
            iGotHit = true;
            Die();
        }
    }

    protected override void Die()
    {
        if (iGotHit) {
            base.Die();
        }
    }
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

