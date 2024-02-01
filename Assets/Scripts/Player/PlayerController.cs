
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;


public class PlayerController : MonoBehaviour
{
    [Header("Controls Settings")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxSpeed;

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
    public static event Action firing;

    // Start is called before the first frame update
    private void Start()
    {
        // Get the Rigidbody2D component only once during initialization
        rb = GetComponent<Rigidbody2D>();
        shipAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        OnFire();
        CheckBoundaries();
    }

    // FixedUpdate is called every fixed frame-rate frame
    private void FixedUpdate()
    {
        MoveShip();
    }

    private void MoveShip()
    {
        // Move forward
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.up * movementSpeed * Time.fixedDeltaTime);
            // Debug.Log("Moving forward");

            // Limit speed
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }

        // Move backward
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.up * movementSpeed * Time.fixedDeltaTime);
            // Debug.Log("Moving backward");

            // Limit speed
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
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
    }

    private void OnFire()
    {
        // Shoot projectiles pressing space
        if (Input.GetKey(KeyCode.Space))
        {
            firing?.Invoke();
        }
    }

    // Destroy player if it collides with asteroid
   private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.tag == "Asteroid")
        {
            //Implement player death logic here, e.g. show game over screen.
           print("Player is dead, got hit by asteroid.");
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