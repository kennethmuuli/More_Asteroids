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
        OffScreenBehaviour();
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(IGotHit(other)) {
            asteroidAnimator.enabled = true;
            t = asteroidAnimator.GetCurrentAnimatorStateInfo(0).length;
            Die(t);
        }
    }
}
