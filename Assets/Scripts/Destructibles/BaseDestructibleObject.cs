using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDestructibleObject : MonoBehaviour
{
    [SerializeField] protected int myScoreValue;
    [Range(0.5f,1f)]
    [SerializeField] protected float minSize = 0.5f;
    [Range(1f,3f)]
    [SerializeField] protected float maxSize = 1.5f;
    [Range(10f,200f)]
    [SerializeField] protected float speed = 50.0f;
    protected Rigidbody2D _rigidbody;
    protected bool iGotHit = false;
    protected Renderer objectRenderer;
    private bool hasBeenInView;
    public static event Action<int> objectDestroyed;
    
    protected virtual void Awake() {
        // Get the Rigidbody2D component attached to this GameObject
        _rigidbody = GetComponent<Rigidbody2D>();
        objectRenderer = GetComponentInChildren<Renderer>();
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            iGotHit = true;
        }
    }

    protected virtual void RandomizeSize(){  
        // Get a random size between the max and min size
        float size = UnityEngine.Random.Range(minSize,maxSize);
        
        // Set the scale of the asteroid based on the 'size' variable
        transform.localScale = Vector2.one * size;
    }

    protected virtual void MoveAndSpin(Vector2 direction, float torque = 2f)
    {
        // Add a force to the Rigidbody2D to set the asteroid in motion
        _rigidbody.AddForce(direction * speed);
        // Add a spin force to the object, if not set then default value is used
        _rigidbody.AddTorque(torque);
    }

    protected virtual void OffScreenBehaviour(){
        // Check whether the renderer is visible and if it hasn't been visible before, set hasBeenInView true
        if(objectRenderer.isVisible && !hasBeenInView) {
            hasBeenInView = true;
        }

        // Check whether the renderer is invisible and if it has been visible before, destroy gameObject
        if (!objectRenderer.isVisible && hasBeenInView)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void Die()
    {
        // Invoke objectDestroyed event sending in myScoreValue for all listeners
        objectDestroyed?.Invoke(myScoreValue);

        // any other logic...

        // Destroy this gameObject | should remain the last thing that's done, as the code won't run after this
        Destroy(gameObject);
    }
}
