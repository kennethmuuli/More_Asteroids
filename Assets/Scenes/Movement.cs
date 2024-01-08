using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSpeed;

    private Rigidbody2D rb;
    private float minX, maxX, minY, maxY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetScreenBoundaries();
    }

    // Update kutsutakse once per frame
    void Update()
    {
        MoveShip();
        ClampPosition();
    }

    void MoveShip()
    {
        float verticalInput = Input.GetAxis("Vertical");
        rb.AddForce(transform.up * verticalInput * movementSpeed * Time.deltaTime);

        float horizontalInput = Input.GetAxis("Horizontal");
        rb.rotation += -horizontalInput * rotationSpeed * Time.deltaTime;
    }

    void ClampPosition()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;
    }

    void SetScreenBoundaries()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            float screenAspect = mainCamera.aspect;
            float cameraSize = mainCamera.orthographicSize;

            minX = mainCamera.transform.position.x - cameraSize * screenAspect;
            maxX = mainCamera.transform.position.x + cameraSize * screenAspect;
            minY = mainCamera.transform.position.y - cameraSize;
            maxY = mainCamera.transform.position.y + cameraSize;
        }
        else
        {
            Debug.LogError("Main camera not found. Ensure there is a camera tagged as 'MainCamera'.");
        }
    }
}
