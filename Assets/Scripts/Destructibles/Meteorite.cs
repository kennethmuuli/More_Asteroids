using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : BaseDestructibleObject
{
    // private Animator meteoriteAnimator;

    private ParticleSystem debrisParticles;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider2D;
    private float t = 2;
    private bool isDying;

    // Called on the frame when a script is enabled
    override protected void Start()
    {
        base.Start();
        // meteoriteAnimator = GetComponentInChildren<Animator>();
        debrisParticles = GetComponentInChildren<ParticleSystem>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();

        RandomizeSize();
        MoveAndSpin(transform.up);
    }

    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);

        if(currentHealth <= 0 && !isDying) {
            isDying = true;
            debrisParticles.Play();
            spriteRenderer.sprite = null;
            circleCollider2D.enabled = false;

            Die(t);
        } return;
    }
}
