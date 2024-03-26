using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.TextCore;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpType powerUpType;
    [SerializeField] private float duration;
    [Tooltip("Time in seconds the powerup lasts a player picks it up.")]
    [SerializeField] private float lifetimeDuration;
    [Tooltip("Time in seconds before the powerup despawns if not picked up.")]
    [SerializeField] private SpriteRenderer iconRenderer, GFXRenderer;
    public static Action<PowerUpType, float, int> PowerUpCollected;
    private Animator pickUpAnimator;

    private void Start() {
        pickUpAnimator = GetComponentInChildren<Animator>();
        Destroy(gameObject, lifetimeDuration);

        StartCoroutine(FadeOut());
    }

    private void OnTriggerEnter2D(Collider2D other) { 
        if (other.CompareTag("Player")) {
            
            PowerUpCollected?.Invoke(powerUpType, duration, other.transform.GetInstanceID());

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
                pickUpAnimator.SetTrigger("despawn_health");
                break;
            case PowerUpType.Revive:
                pickUpAnimator.SetTrigger("despawn_revive");
                break;
            default:
                break;
        }
    }

    private IEnumerator FadeOut () {
        float delay = lifetimeDuration - 3f;
        
        yield return new WaitForSeconds(delay);

        float timeElapsed = 0;

        while (timeElapsed < 3f){
            float t = timeElapsed / 3f;
            iconRenderer.material.color = new Color(iconRenderer.color.r, iconRenderer.color.g, iconRenderer.color.b, Mathf.Lerp(1,0,t));
            GFXRenderer.material.color = new Color(GFXRenderer.color.r, GFXRenderer.color.g, GFXRenderer.color.b, Mathf.Lerp(1,0,t));
            timeElapsed += Time.deltaTime;

            yield return null;
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
