using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpType powerUpType;
    [SerializeField] private float duration;
    public static Action<PowerUpType, float> powerUpCollected;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            powerUpCollected?.Invoke(powerUpType, duration);

            Destroy(gameObject, 0.1f);
        }
    }
}

public enum PowerUpType {
    Laser,
    Shield,
    Speed,
    Health
}
