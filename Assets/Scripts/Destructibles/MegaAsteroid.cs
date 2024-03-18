using System.Collections.Generic;
using UnityEngine;

public class MegaAsteroid : BaseDestructibleObject
{
    private Animator asteroidAnimator;
    [SerializeField]private CircleCollider2D circleCollider2D;
    [SerializeField] private GameObject metalColliders;
    [SerializeField] private List<Sprite> breakingStates;
    private SpriteRenderer spriteRenderer;
    private float t;
    private bool isDying;

    // Called on the frame when a script is enabled
    override protected void Start()
    {
        base.Start();
        asteroidAnimator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        RandomizeSize();
        MoveAndSpin(transform.up);
    }

    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
        // print(currentHealth);
        visualizeBreaking();
        if(currentHealth <= 0 && !isDying) {
            isDying = true;
            circleCollider2D.enabled = false;
            metalColliders.SetActive(false);
            asteroidAnimator.enabled = true;
            t = asteroidAnimator.GetCurrentAnimatorStateInfo(0).length;
            Die(t);
        } return;
    }

    private void visualizeBreaking(){
        float remainingHealthPercentage = (float)currentHealth / (float)health;

        if((remainingHealthPercentage <= 0.5) && remainingHealthPercentage > 0.25) {
            spriteRenderer.sprite = breakingStates[0];
        }else if(remainingHealthPercentage <= 0.25) {
            spriteRenderer.sprite = breakingStates[1];
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + circleCollider2D.offset, circleCollider2D.radius);
    }
}
