using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


public class Movement : MonoBehaviour
{
    public float rotationSpeed;
    public float movementSpeed;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component only once during initialization
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move forward
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(transform.up * movementSpeed * Time.deltaTime);

        // Move backward
        if (Input.GetKey(KeyCode.S))
            rb.AddForce(-transform.up * movementSpeed * Time.deltaTime);

        // Rotate left
        if (Input.GetKey(KeyCode.A))
            rb.rotation += rotationSpeed * Time.deltaTime;

        // Rotate right
        if (Input.GetKey(KeyCode.D))
            rb.rotation -= rotationSpeed * Time.deltaTime;
    }
}

