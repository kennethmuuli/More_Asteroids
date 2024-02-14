using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;
    
    // Destroy projectile if it collides with asteroid
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.tag == "Asteroid")
        {
            other.transform.gameObject.GetComponent<BaseDestructibleObject>().TakeDamage(damageAmount);

            Destroy(gameObject);
        }
    }
}
