using UnityEngine;

public class Asteroid : BaseDestructibleObject
{
    private Animator asteroidAnimator;
    private float t;
    private bool isDying;

    // Called on the frame when a script is enabled
    override protected void Start()
    {
        base.Start();
        asteroidAnimator = GetComponentInChildren<Animator>();
        RandomizeSize();
        MoveAndSpin(transform.up);
    }

    private void Update()
    {
        OffScreenBehaviour();
    }

    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);

        if(currentHealth <= 0 && !isDying) {
            isDying = true;
            asteroidAnimator.enabled = true;
            t = asteroidAnimator.GetCurrentAnimatorStateInfo(0).length;
            Die(t);
        } return;
    }
}
