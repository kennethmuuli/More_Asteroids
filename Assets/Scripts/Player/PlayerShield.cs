using System;
using UnityEngine;

public class PlayerShield : PowerUpComponent
{
    [SerializeField] private CircleCollider2D shieldCollider;
    [SerializeField] private GameObject shieldGFX;
    private bool componentsOnOff;

    override protected void Update() {
        if (!powerUpEngaged && componentsOnOff == true) {
            OnOffComponents(false);
        } else if (powerUpEngaged && componentsOnOff == false) {
            OnOffComponents(true);
        } 

        base.Update();
    }

    // Destroy player if it collides with asteroid
    private void OnCollisionEnter2D(Collision2D other) 
    { 

        Collider2D col = other.collider;

        if(shieldCollider.IsTouching(col)) {
            if (shieldCollider && (col.tag == "Asteroid" || col.tag == "Meteorite") && powerUpEngaged)
            {
                    other.gameObject.GetComponent<BaseDestructibleObject>().TakeDamage(10);
            }
        } return;

        
    }

    private void OnOffComponents (bool onOff) {
        shieldCollider.enabled = onOff;
        shieldGFX.SetActive(onOff);
        componentsOnOff = onOff;
    }

    

}
