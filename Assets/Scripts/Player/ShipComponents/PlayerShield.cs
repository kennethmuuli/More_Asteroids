using UnityEngine;

public class PlayerShield : PowerUpComponent
{
    [SerializeField] private CircleCollider2D shieldCollider;
    [SerializeField] private GameObject shieldGFX;
    private Rigidbody2D rb;
    private bool componentsOnOff;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
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

        Collider2D col = other.collider;

        if(shieldCollider.IsTouching(col)) {
            
            if (shieldCollider && powerUpEngaged)
            {
                if (col.tag == "Asteroid" || col.tag == "Meteorite")
                {
                    other.gameObject.GetComponent<BaseDestructibleObject>().TakeDamage(4);
                }
                if (col.tag == "Megaroid" || col.tag == "MegaroidShield")
                {
                    PowerUp.PowerUpCollected(PowerUpType.Shield,0.1f,instanceID);
                    OnOffComponents(false);
                    other.gameObject.GetComponent<BaseDestructibleObject>().TakeDamage(4);
                }
            }
        } return;
    }

    private void OnOffComponents (bool onOff) {
        shieldCollider.enabled = onOff;
        shieldGFX.SetActive(onOff);
        componentsOnOff = onOff;
    }

    

}
