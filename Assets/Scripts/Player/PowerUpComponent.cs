using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpComponent : MonoBehaviour
{
    [SerializeField] private PowerUpType myPowerUpType;
    [Tooltip("Designate, which type of power up this is.")]
    protected bool powerUpEngaged;
    protected float powerUpDuration;
    private int instanceID;

    private void Awake() {
        instanceID = transform.GetInstanceID();
    }
    private void OnEnable() {
        PowerUp.powerUpCollected += OnPowerUpCollected;
    }
    private void OnDisable() {
        PowerUp.powerUpCollected -= OnPowerUpCollected;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(Time.time > powerUpDuration) {
            powerUpEngaged = false;
        }
    }

    private void OnPowerUpCollected (PowerUpType powerUpType, float duration, int instanceIDToCheck) {
        if (powerUpType == myPowerUpType && instanceID == instanceIDToCheck)
        {
            powerUpEngaged = true;
            powerUpDuration = Time.time + duration;
        } return;
    }
}
