using UnityEngine;

public class Asteroid : BaseDestructibleObject
{
    // Public variables for asteroid properties
    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float speed = 50.0f;
    public float maxLifetime = 3.0f;

    // Reference to the Rigidbody2D component
    private Rigidbody2D _rigidbody;

    // Called when the script instance is being loaded
    private void Awake()
    {
        // Get the Rigidbody2D component attached to this GameObject
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Called on the frame when a script is enabled
    private void Start()
    {
        // Set a random rotation to the asteroid
        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);

        // Set the scale of the asteroid based on the 'size' variable
        transform.localScale = Vector3.one * size;

        // Set the mass of the Rigidbody2D based on the 'size' variable
        _rigidbody.mass = size;

        MoveAndSpin(transform.up);
    }

    private void Update() {
        if (iGotHit)
        {
            Die();
        }
    }

    // Example method that may trigger the objectDestroyed event
    public void MoveAndSpin(Vector2 direction, float torque = 2f)
    {
        // Add a force to the Rigidbody2D to set the asteroid in motion
        _rigidbody.AddForce(direction * speed);
        // Add a spin force to the object, if not set then default value is used
        _rigidbody.AddTorque(torque);
        // Destroy the asteroid after a specified lifetime
        Destroy(gameObject, maxLifetime);
    }

}
