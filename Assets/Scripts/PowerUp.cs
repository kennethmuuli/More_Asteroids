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
    public static Action<PowerUpType, float, int> powerUpCollected;
    private Animator pickUpAnimator;

    private void Start() {
        pickUpAnimator = GetComponentInChildren<Animator>();
        Destroy(gameObject, lifetimeDuration);
    }

    private void OnTriggerEnter2D(Collider2D other) { 
        if (other.CompareTag("Player")) {
            
            powerUpCollected?.Invoke(powerUpType, duration, other.transform.GetInstanceID());

            ChooseDespawnAnimation();
            float t = pickUpAnimator.GetCurrentAnimatorStateInfo(0).length;
            iconRenderer.enabled = false;

            Destroy(gameObject, t);
        }
    }

    private void ChooseDespawnAnimation(){
        switch (powerUpType)
        {
            case PowerUpType.Laser:
                pickUpAnimator.SetTrigger("despawn_laser");
                break;
            case PowerUpType.Shield:
                pickUpAnimator.SetTrigger("despawn_shield");
                break;
            case PowerUpType.Speed:
                pickUpAnimator.SetTrigger("despawn_speed");
                break;
            case PowerUpType.Health:
                pickUpAnimator.SetTrigger("despawn_speed");
                break;
            case PowerUpType.Revive:
                pickUpAnimator.SetTrigger("despawn_speed");
                break;
            default:
                break;
        }
    }
}

public enum PowerUpType {
    Laser,
    Shield,
    Speed,
    Health,
    Revive
}
