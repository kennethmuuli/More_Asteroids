using System;
using UnityEngine;

public class PlayerHull : PowerUpComponent
{
    [SerializeField] private CircleCollider2D shieldCollider;
    [SerializeField] private GameObject shieldGFX;
    [SerializeField] private int hullHealth = 3;
    public static Action<int, int> PlayerHealthUpdated;
    private int currentHullHealth;
    private bool componentsOnOff;

    private void Start() {
        currentHullHealth = hullHealth;
    }

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
       if (other.collider.tag == "Asteroid" || other.collider.tag == "Meteorite")
       {
            if(!powerUpEngaged)
            {
                other.gameObject.GetComponent<BaseDestructibleObject>().TakeDamage(10);
                UpdatePlayerHealth(-1);
                
            } else if (powerUpEngaged) {
                other.gameObject.GetComponent<BaseDestructibleObject>().TakeDamage(10);
                
            }
       }
    }

    private void OnOffComponents (bool onOff) {
        shieldCollider.enabled = onOff;
        shieldGFX.SetActive(onOff);
        componentsOnOff = onOff;
    }

    private void UpdatePlayerHealth(int changeAmount) {
        currentHullHealth = currentHullHealth + changeAmount;

        PlayerHealthUpdated?.Invoke(instanceID, currentHullHealth);

        if (currentHullHealth <= 0)
        {
            shieldCollider.enabled = false;
            shieldGFX.SetActive(false);
            GameManager.instance.PlayerDied();
            gameObject.SetActive(false);
        }
    }

}
