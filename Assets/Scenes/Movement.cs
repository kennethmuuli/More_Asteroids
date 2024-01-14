
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using System;


public class Movement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSpeed;

    private Rigidbody2D rb;


    public static event Action firing;

    // Set these values to define the boundaries
    private float minX = -26f;
    private float maxX = 26f;
    private float minY = -13f;
    private float maxY = 13f;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component only once during initialization
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        OnFire();
        CheckBoundaries();
    }
    
    // FixedUpdate is called every fixed frame-rate frame
    void FixedUpdate()
    {
        MoveShip();
    }

    void MoveShip()
    {
        // Move forward
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.up * movementSpeed * Time.deltaTime);
            // Debug.Log("Moving forward");
        }

        // Move backward
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.up * movementSpeed * Time.deltaTime);
            // Debug.Log("Moving backward");
        }

        // Rotate left
        if (Input.GetKey(KeyCode.A))
        {
            rb.rotation += rotationSpeed * Time.deltaTime;
            // Debug.Log("Rotating left");
        }

        // Rotate right
        if (Input.GetKey(KeyCode.D))
        {
            rb.rotation -= rotationSpeed * Time.deltaTime;
            // Debug.Log("Rotating right");
        }
    }
    
    void OnFire()
    {   
        // Shoot projectiles pressing space
        if(Input.GetKey(KeyCode.Space))
        {
            firing?.Invoke();
        }
    }
}

    void CheckBoundaries()
    {
        // Clamp the position to stay within the defined boundaries
        float clampedX = Mathf.Clamp(rb.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(rb.position.y, minY, maxY);

        // Update the Rigidbody2D position
        rb.position = new Vector2(clampedX, clampedY);

        // Debug the clamped position if you want 
        //  Debug.Log($"Clamped Position: ({clampedX}, {clampedY})");
    }
}
