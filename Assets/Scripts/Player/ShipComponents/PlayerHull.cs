using UnityEngine;
using System;

public class PlayerHull : PowerUpComponent
{
    [SerializeField] private PolygonCollider2D hullCollider;
    [SerializeField] private int maxHullHealth = 3;
    public static Action<int, int> OnPlayerHealthUpdated;
    private int currentHullHealth;

    private void Start() {
        currentHullHealth = maxHullHealth;
        SetStartPosition();
        // Calling it so that player health would not fall into negative numbers, playerReviver will take care of adding the one respawn health;
        UpdatePlayerHealth(0);
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

            //Stop any potential continuous sounds
            AudioManager.StopSFX(SFXName.ShipAccelerate,instanceID);
            AudioManager.StopSFX(SFXName.ShootLaser,instanceID);
            
            currentHullHealth = 0;
            gameObject.SetActive(false);
        }
    }
}
