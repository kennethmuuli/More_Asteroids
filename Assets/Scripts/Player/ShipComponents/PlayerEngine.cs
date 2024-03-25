using UnityEngine;

[RequireComponent(typeof(PlayerInputReader))]
public class PlayerEngine : PowerUpComponent
{
    [Header("Movement Settings")]
    [SerializeField] private float baseRotationSpeed;
    [SerializeField] private float baseTurnSpeed;
    [SerializeField] private float baseMovementSpeed;
    [SerializeField] private float shipMaxSpeed;
    [Header("Power Up Movement Settings")]
    [SerializeField] private float PURotationSpeed;
    [SerializeField] private float PUTurnSpeed;
    [SerializeField] private float PUMovementSpeed;
    [Header("Boost Settings")]
    [SerializeField] private float boostForce;
    [SerializeField] private float boostCooldown;
    private float nextBoostTime;

    [Header("Game Area")]
    [SerializeField] private float gameAreaPadding;
    [Tooltip("Distance from view port edge in world units to bring the game area inside the view port and not allow a part of the ship to fly off screen.")]
    [SerializeField] private bool showGameArea;
    [Tooltip("Check to visualize game area in the scene view")]
    float gameAreaWidth;
    float gameAreaHeight;

    private Animator shipAnimator;
    private Rigidbody2D rb;
    private PlayerInputReader movementInput;

    protected override void OnDisable()
    {
        base.OnDisable();

        transform.SetPositionAndRotation(Vector2.zero, Quaternion.identity);

    }

    // Start is called before the first frame update

    private void Start()
    {
        // Get the Rigidbody2D component only once during initialization
        rb = GetComponent<Rigidbody2D>();
        shipAnimator = GetComponentInChildren<Animator>();
        movementInput = GetComponent<PlayerInputReader>();
    }

    // Update is called once per frame
    override protected void Update()
    {
        CheckBoundaries();

        base.Update();
    }

    private void FixedUpdate() {
        if(powerUpEngaged) {
                MoveShip(PUMovementSpeed, shipMaxSpeed, PURotationSpeed, PUTurnSpeed);
        } else {MoveShip(baseMovementSpeed, shipMaxSpeed, baseRotationSpeed, baseTurnSpeed);}  

        if (movementInput.IsBoosting)
        {
           Boost(boostForce);    
        }
    }

    private void MoveShip(float movementSpeed, float maxSpeed, float rotationSpeed, float turnSpeed)
    {
        float verticalInput = movementInput.MovementVector.y;
        float horizontalInput = movementInput.MovementVector.x;
        
        // Move forward
        if (verticalInput != 0)
        {
            rb.AddForce(verticalInput * transform.up * movementSpeed * Time.fixedDeltaTime);
        }

        // Rotate left
        if (horizontalInput < 0)
        {
            shipAnimator.SetBool("turnLeft", true);

            if (verticalInput < 0)
            {
                rb.rotation += rotationSpeed * Time.fixedDeltaTime;
                rb.AddForce(-transform.right * turnSpeed * Time.fixedDeltaTime);
            } else {
                rb.rotation += rotationSpeed * Time.fixedDeltaTime;
            }
        } else shipAnimator.SetBool("turnLeft", false);
        
        if (horizontalInput > 0)
        {
            shipAnimator.SetBool("turnRight", true);

            if (verticalInput > 0)
            {
                rb.rotation -= rotationSpeed * Time.fixedDeltaTime;
                rb.AddForce(transform.right * turnSpeed * Time.fixedDeltaTime);
            } else {
                rb.rotation -= rotationSpeed * Time.fixedDeltaTime;
            }
        } else shipAnimator.SetBool("turnRight", false);

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }

    private void Boost(float boostForce) {
        
        if (Time.time > nextBoostTime)
        {
            rb.AddForce(transform.up * boostForce, ForceMode2D.Impulse);
            nextBoostTime = Time.time + boostCooldown;
        }
    }

    private void CheckBoundaries()
    {
        // Get the main camera
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            // Calculate the orthographic size of the camera
            float cameraOrthographicSize = mainCamera.orthographicSize;
            float cameraAspect = mainCamera.aspect;

            // Calculate the boundaries based on camera view
            gameAreaWidth = cameraOrthographicSize * cameraAspect - gameAreaPadding;
            gameAreaHeight = cameraOrthographicSize - gameAreaPadding;

            // Clamp the position to stay within the calculated boundaries
            float clampedX = Mathf.Clamp(rb.position.x, -gameAreaWidth, gameAreaWidth);
            float clampedY = Mathf.Clamp(rb.position.y, -gameAreaHeight, gameAreaHeight);

            // Update the Rigidbody2D position
            rb.position = new Vector2(clampedX, clampedY);
        }
        else
        {
            Debug.LogWarning("Main camera not found.");
        }
    }

# if UNITY_EDITOR
    private void OnDrawGizmos() {
        if (showGameArea)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(new Vector2(0,0),new Vector2(gameAreaWidth*2,gameAreaHeight*2)); 
        }   
    }

# endif
}

