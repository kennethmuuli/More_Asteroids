using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Event to notify when an object is destroyed, with an associated score
    public delegate void ObjectDestroyedHandler(int scoreToAdd);
    public static event ObjectDestroyedHandler objectDestroyed;

    // Array of sprites for the asteroid
    public Sprite[] sprites;

    // Reference to the SpriteRenderer component
    private SpriteRenderer _spriteRenderer;

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
        // Get the SpriteRenderer component attached to this GameObject
        _spriteRenderer = GetComponent<SpriteRenderer>();
        // Get the Rigidbody2D component attached to this GameObject
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Called on the frame when a script is enabled
    private void Start()
    {
        // Set a random sprite from the array to the SpriteRenderer
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        // Set a random rotation to the asteroid
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);

        // Set the scale of the asteroid based on the 'size' variable
        this.transform.localScale = Vector3.one * size;

        // Set the mass of the Rigidbody2D based on the 'size' variable
        _rigidbody.mass = this.size;
    }

    // Example method that may trigger the objectDestroyed event
    public void SetTrajectory(Vector2 direction)
    {
        // Add a force to the Rigidbody2D to set the asteroid in motion
        _rigidbody.AddForce(direction * this.speed);
        // Destroy the asteroid after a specified lifetime
        Destroy(this.gameObject, this.maxLifetime);
    }

    // Destroy asteroid if it collides with projectile
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Projectile")
        {
            Die();
        }
    }

    // Notify the destruction of the object and perform any necessary actions
    private void Die()
    {
        // Notify subscribers that the object is destroyed and provide a score
        if (objectDestroyed != null)
        {
            objectDestroyed.Invoke(10); // You can replace 10 with the appropriate score value
        }

        // Add any other cleanup or game logic here
        Destroy(gameObject);
    }
}
