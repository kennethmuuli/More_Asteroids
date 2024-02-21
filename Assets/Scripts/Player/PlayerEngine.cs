
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;


public class PlayerEngine : PowerUpComponent
{
    [Header("Movement Settings")]
    [SerializeField] private float baseRotationSpeed;
    [SerializeField] private float baseMovementSpeed;
    [SerializeField] private float shipMaxSpeed;
    [Header("Power Up Movement Settings")]
    [SerializeField] private float PURotationSpeed;
    [SerializeField] private float PUMovementSpeed;
    [Header("Boost Settings")]
    [SerializeField] private float boostForce;
    [SerializeField] private float boostCooldown;
    private float nextBoostTime;

    // Set these values to define the boundaries
    [Header("Game Area")]
    [SerializeField] private float gameAreaWidth = 26f;
    [Tooltip("Game area bounding box width/2 in world units")]
    [SerializeField] private float gameAreaHeight = 13f;
    [Tooltip("Game area bounding box height/2 in world units")]
    [SerializeField] private bool showGameArea;
    [Tooltip("Check to visualize game area in the scene view")]

    private Animator shipAnimator;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        // Get the Rigidbody2D component only once during initialization
        rb = GetComponent<Rigidbody2D>();
        shipAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    override protected void Update()
    {
        CheckBoundaries();

        base.Update();
    }

    private void FixedUpdate() {
        if(powerUpEngaged) {
                MoveShip(PUMovementSpeed, shipMaxSpeed, PURotationSpeed);
        } else {MoveShip(baseMovementSpeed, shipMaxSpeed, baseRotationSpeed);}  
    }

    private void MoveShip(float movementSpeed, float maxSpeed, float rotationSpeed)
    {
        // Move forward
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.up * movementSpeed * Time.fixedDeltaTime);
        }

        // Move backward
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.up * movementSpeed * Time.fixedDeltaTime);
            // Debug.Log("Moving backward");
        }

        // Rotate left
        if (Input.GetKey(KeyCode.A))
        {
            rb.rotation += rotationSpeed * Time.fixedDeltaTime;
            shipAnimator.SetBool("turnLeft", true);
            // Debug.Log("Rotating left");
        }
        else
        {
            shipAnimator.SetBool("turnLeft", false);
        }

        // Rotate right
        if (Input.GetKey(KeyCode.D))
        {
            rb.rotation -= rotationSpeed * Time.fixedDeltaTime;
            shipAnimator.SetBool("turnRight", true);
            // Debug.Log("Rotating right");
        }
        else
        {
            shipAnimator.SetBool("turnRight", false);
        }

        Boost(boostForce);

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }

    private void Boost(float boostForce) {
        
        if (Time.time > nextBoostTime)
        {

            if (Input.GetKey(KeyCode.I))
            {
                rb.AddForce(transform.up * boostForce, ForceMode2D.Impulse);
                nextBoostTime = Time.time + boostCooldown;
            }

        }
    }

    private void CheckBoundaries()
    {
        /* TODO: Would make sense to calculate the boundaries from the main camera field of view */
        // Clamp the position to stay within the defined boundaries
        float clampedX = Mathf.Clamp(rb.position.x, -gameAreaWidth, gameAreaWidth);
        float clampedY = Mathf.Clamp(rb.position.y, -gameAreaHeight, gameAreaHeight);

        // Update the Rigidbody2D position
        rb.position = new Vector2(clampedX, clampedY);

        // Debug the clamped position if you want 
        //  Debug.Log($"Clamped Position: ({clampedX}, {clampedY})");
    }

# if UNITY_EDITOR
    private void OnDrawGizmos() {
        if (showGameArea)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(new Vector2(0,0),new Vector2(gameAreaWidth*2,gameAreaHeight*2)); 
        }   
    }
}

# endif