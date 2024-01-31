using System.Collections;
using UnityEngine;

public class Asteroid : BaseDestructibleObject
{
    private Animator asteroidAnimator;
    private float t;

    // Called on the frame when a script is enabled
    private void Start()
    {
        asteroidAnimator = GetComponentInChildren<Animator>();
        RandomizeSize();
        MoveAndSpin(transform.up);
    }

    private void Update()
    {
        if (iGotHit)
        {
            asteroidAnimator.enabled = true;
            t = asteroidAnimator.GetCurrentAnimatorStateInfo(0).length;
            Die(t);
        }

        OffScreenBehaviour();
    }

    private IEnumerator DestroyDelay(float delay)
    {       
            yield return new WaitForSeconds(delay);
            //Die();        
    }
}
