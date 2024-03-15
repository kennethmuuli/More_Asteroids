using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHull : PowerUpComponent
{
    [SerializeField] private PolygonCollider2D hullCollider;
    [SerializeField] private int hullHealth = 3;
    public static Action<int, int> OnPlayerHealthUpdated;
    private int currentHullHealth;

    private void Start() {
        currentHullHealth = hullHealth;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    { 
        Collider2D col = other.collider;

        if(hullCollider.IsTouching(col)) {
            if (col.tag == "Asteroid" || col.tag == "Meteorite")
            {
                    other.gameObject.GetComponent<BaseDestructibleObject>().TakeDamage(10);
                    UpdatePlayerHealth(-1);
            }
        } return;

    }


    private void UpdatePlayerHealth(int changeAmount) {
        currentHullHealth = currentHullHealth + changeAmount;

        OnPlayerHealthUpdated?.Invoke(instanceID, currentHullHealth);

        if (currentHullHealth <= 0)
        {
            GameManager.instance.PlayerDied();
            gameObject.SetActive(false);
            currentHullHealth = 0;
        }
    }
}
