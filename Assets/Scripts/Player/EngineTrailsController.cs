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
    // Start is called before the first frame update
    void Start()
    {
        shipRb = GetComponentInParent<Rigidbody2D>();
    }

    
    void Update()
    {
        ChangeEngineTrailColor();
    }

    private void ChangeEngineTrailColor() {
        
        float shipMagnitude = shipRb.velocity.magnitude;

        print(shipMagnitude);
        if(shipMagnitude > normalMagnitudeCap) {
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
}
