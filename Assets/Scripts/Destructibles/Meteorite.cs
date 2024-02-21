using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : BaseDestructibleObject
{
    // private Animator meteoriteAnimator;
    private float t;
    private bool isDying;

    // Called on the frame when a script is enabled
    override protected void Start()
    {
        base.Start();
        // meteoriteAnimator = GetComponentInChildren<Animator>();
        RandomizeSize();
        MoveAndSpin(transform.up);
    }

    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);

        if(currentHealth <= 0 && !isDying) {
            isDying = true;
            // meteoriteAnimator.enabled = true;
            // t = asteroidAnimator.GetCurrentAnimatorStateInfo(0).length;
            Die(t);
        } return;
    }
}
