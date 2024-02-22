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
       if (other.collider.tag == "Asteroid" || other.collider.tag == "Meteorite")
       {
            if(!powerUpEngaged)
            {
                shieldCollider.enabled = false;
                shieldGFX.SetActive(false);
                //Implement player death logic here, e.g. show game over screen.
                print("Player is dead, got hit by asteroid.");
            } else if (powerUpEngaged) {
                
                other.gameObject.GetComponent<BaseDestructibleObject>().TakeDamage(1);
            }
       }
    }

    private void OnOffComponents (bool onOff) {
        shieldCollider.enabled = onOff;
        shieldGFX.SetActive(onOff);
        componentsOnOff = onOff;
    }

}
