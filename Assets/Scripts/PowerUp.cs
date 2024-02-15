using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpType powerUpType;
    [SerializeField] private float duration;
    public static Action<PowerUpType, float> powerUpCollected;
    private Animator pickUpAnimator;

    private void Start() {
        pickUpAnimator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            powerUpCollected?.Invoke(powerUpType, duration);

            pickUpAnimator.SetTrigger("despawn");
            float t = pickUpAnimator.GetCurrentAnimatorStateInfo(0).length;

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
