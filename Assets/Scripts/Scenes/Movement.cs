using UnityEngine;

public class Movement : MonoBehaviour
{
    // Rotation speed of the spaceship (set in the Unity Inspector)
    [SerializeField] private float rotationSpeed;

    // Movement speed of the spaceship (set in the Unity Inspector)
    [SerializeField] private float movementSpeed;

    // Reference to the Rigidbody2D component of the spaceship
    private Rigidbody2D rb;

    // Variables to store screen boundaries
    private float minX, maxX, minY, maxY;

    // Called when the script is first loaded
    void Start()
    {
        // Get the Rigidbody2D component of the spaceship
        rb = GetComponent<Rigidbody2D>();

        // Set screen boundaries based on the camera's position and size
        SetScreenBoundaries();
    }

    // Called every frame
    void Update()
    {
        // Call the function to move the spaceship
        MoveShip();
    }

    // Function to handle the movement of the spaceship
    void MoveShip()
    {
        // Get vertical input (up/down arrow keys or joystick)
        float verticalInput = Input.GetAxis("Vertical");

        // Apply force to move the spaceship vertically
        rb.AddForce(transform.up * verticalInput * movementSpeed * Time.deltaTime);

        // Get horizontal input (left/right arrow keys or joystick)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Rotate the spaceship based on horizontal input
        rb.rotation += -horizontalInput * rotationSpeed * Time.deltaTime;

        // Keep the spaceship within the boundaries of the screen
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;
    }

    // Function to set the screen boundaries based on the camera's position and size
    void SetScreenBoundaries()
    {
        // Get the main camera in the scene
        Camera mainCamera = Camera.main;

        // Check if the main camera is found
        if (mainCamera != null)
        {
            // Calculate screen boundaries based on camera position and size
            float screenAspect = mainCamera.aspect;
            float cameraSize = mainCamera.orthographicSize;

            minX = mainCamera.transform.position.x - cameraSize * screenAspect;
            maxX = mainCamera.transform.position.x + cameraSize * screenAspect;
            minY = mainCamera.transform.position.y - cameraSize;
            maxY = mainCamera.transform.position.y + cameraSize;
        }
        else
        {
            // Log an error if the main camera is not found
            Debug.LogError("Main camera not found. Ensure there is a camera tagged as 'MainCamera'.");
        }
    }
}
