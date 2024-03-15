using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpComponent : MonoBehaviour
{
    [SerializeField] protected PowerUpType myPowerUpType;
    [Tooltip("Designate, which type of power up this is.")]
    protected bool powerUpEngaged;
    protected float powerUpDuration;
    protected int instanceID;

    private void Awake() {
        instanceID = transform.GetInstanceID();
    }
    protected virtual void OnEnable() {
        PowerUp.PowerUpCollected += OnPowerUpCollected;
    }
    protected virtual void OnDisable() {
        PowerUp.PowerUpCollected -= OnPowerUpCollected;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(Time.time > powerUpDuration) {
            powerUpEngaged = false;
        }
    }

    protected virtual void OnPowerUpCollected (PowerUpType powerUpType, float duration, int instanceIDToCheck) {
        if (powerUpType == myPowerUpType && instanceID == instanceIDToCheck)
        {
            powerUpEngaged = true;
            powerUpDuration = Time.time + duration;
        } return;
    }
}
