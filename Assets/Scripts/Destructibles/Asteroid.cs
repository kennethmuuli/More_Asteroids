using UnityEngine;

public class Asteroid : BaseDestructibleObject
{

    // Called when the script instance is being loaded
    private void Awake()
    {
        // Get the Rigidbody2D component attached to this GameObject
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Called on the frame when a script is enabled
    private void Start()
    {
        RandomizeSizeAndRotation();        

        MoveAndSpin(transform.up);
    }

    private void Update() {
        if (iGotHit)
        {
            Die();
        }

        DespawnIfInvisible();
    }
    

}
