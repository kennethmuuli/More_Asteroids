using UnityEngine;

public class Asteroid : BaseDestructibleObject
{
    private Animator asteroidAnimator;
    private CircleCollider2D circleCollider2D;
    private float t;
    private bool isDying;

    // Called on the frame when a script is enabled
    override protected void Start()
    {
        base.Start();
        asteroidAnimator = GetComponentInChildren<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        RandomizeSize();
        MoveAndSpin(transform.up);
    }

    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);

        if(currentHealth <= 0 && !isDying) {
            isDying = true;
            circleCollider2D.enabled = false;
            asteroidAnimator.enabled = true;
            t = asteroidAnimator.GetCurrentAnimatorStateInfo(0).length;
            Die(t);
        } return;
    }
}
