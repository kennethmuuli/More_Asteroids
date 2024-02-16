using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpType powerUpType;
    [SerializeField] private float duration;
    [Tooltip("Time in seconds the powerup lasts a player picks it up.")]
    [SerializeField] private float lifetimeDuration;
    [Tooltip("Time in seconds before the powerup despawns if not picked up.")]
    [SerializeField] private SpriteRenderer iconRenderer;
    public static Action<PowerUpType, float> powerUpCollected;
    private Animator pickUpAnimator;

    private void Start() {
        pickUpAnimator = GetComponentInChildren<Animator>();
        Destroy(gameObject, lifetimeDuration);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            powerUpCollected?.Invoke(powerUpType, duration);

            pickUpAnimator.SetTrigger("despawn");
            float t = pickUpAnimator.GetCurrentAnimatorStateInfo(0).length;
            iconRenderer.enabled = false;

            Destroy(gameObject, t);
        }
    }
}

public enum PowerUpType {
    Laser,
    Shield,
    Speed,
    Health
}
