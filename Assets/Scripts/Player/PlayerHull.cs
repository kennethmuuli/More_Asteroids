using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class PlayerHull : PowerUpComponent
{
    [SerializeField] private PolygonCollider2D hullCollider;
    [SerializeField] private int maxHullHealth = 3;
    public static Action<int, int> OnPlayerHealthUpdated;
    private int currentHullHealth;

    private void Start() {
        currentHullHealth = maxHullHealth;
        SetStartPosition();
    }

    protected override void Update()
    {
        if(powerUpEngaged) {
            UpdatePlayerHealth(1);
            powerUpEngaged = false;
        }
        base.Update();
    }

    private void SetStartPosition() {
        transform.position = Vector2.zero;
        transform.rotation = Quaternion.identity;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    { 
        Collider2D col = other.collider;

        if(hullCollider.IsTouching(col)) {
            if (col.tag == "Asteroid" || col.tag == "Meteorite")
            {
                other.gameObject.GetComponent<BaseDestructibleObject>().TakeDamage(10);
                UpdatePlayerHealth(-1);
            } else if (col.tag == "Megaroid" || col.tag == "MegaroidShield") {
                other.gameObject.GetComponent<BaseDestructibleObject>().TakeDamage(10);
                UpdatePlayerHealth(-3);
            }
        } return;
    }

    private void UpdatePlayerHealth(int changeAmount) {

        if (currentHullHealth + changeAmount > maxHullHealth) {
            currentHullHealth = maxHullHealth;
        } else currentHullHealth = currentHullHealth + changeAmount;

        OnPlayerHealthUpdated?.Invoke(instanceID, currentHullHealth);

        if (currentHullHealth <= 0)
        {
            GameManager.instance.PlayerDied();
            gameObject.SetActive(false);
            currentHullHealth = 0;
        }
    }
}
