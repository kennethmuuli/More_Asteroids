using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EngineTrailsController : MonoBehaviour
{
    
    [SerializeField] private ParticleSystem[] engineTrails;
    [SerializeField] private Color normalSpeedColor, highSpeedColor, maxSpeedColor;
    [SerializeField] private Color currentTrailColor;
    [SerializeField] private float normalMagnitudeCap = 5, boostMagnitudeCap;
    private Rigidbody2D shipRb;
    private bool isPoweredUp;
    private float poweredUpTime;
    private int instanceID;
    
    private void OnEnable() {
        PowerUp.powerUpCollected += IsPoweredUp;
    }
    private void OnDisable() {
        PowerUp.powerUpCollected -= IsPoweredUp;
    }
    
    private void Awake() {
        instanceID = transform.GetInstanceID();
    }

    void Start()
    {
        shipRb = GetComponentInParent<Rigidbody2D>();
    }

    
    void Update()
    {
        ChangeEngineTrailColor();

        if(Time.time > poweredUpTime) {
            isPoweredUp = false;
        }
    }

    private void ChangeEngineTrailColor() {
        
        float shipMagnitude = shipRb.velocity.magnitude;

        if(isPoweredUp) {
            float t = shipMagnitude / boostMagnitudeCap;
            currentTrailColor = Color.Lerp(highSpeedColor,maxSpeedColor,t);
        } else {
            float t = shipMagnitude / normalMagnitudeCap;
            currentTrailColor = Color.Lerp(normalSpeedColor,highSpeedColor,t);
        }

        foreach (ParticleSystem engineTrail in engineTrails)
        {
            var main = engineTrail.main;
            main.startColor = currentTrailColor;
        }
    }

    private void IsPoweredUp(PowerUpType powerUpType, float duration, int instanceIDToCheck){
        if (powerUpType == PowerUpType.Speed && instanceID == instanceIDToCheck)
        {
            isPoweredUp = true;
            poweredUpTime = Time.time + duration;
        }
    }
}
