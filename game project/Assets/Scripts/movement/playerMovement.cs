using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSmoothTime = 0.1f;
    public float jumpForce = 8f; // Force applied when jumping
    public float runSpeed = 6f; // Speed multiplier when running
    public float mass = 70f;

    float turnSmoothVelocity;
    bool isGrounded; // Check if the player is on the ground

    Rigidbody rb; // Reference to the Rigidbody component

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
        rb.useGravity = true;
        rb.mass= mass;
        Vector3 customGravity = Physics.gravity * 2.0f; // Modify the global gravity
        Physics.gravity = customGravity;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed; // Increase movement speed when holding Left Shift
        }
        else
        {
            moveSpeed = 5f; // Reset movement speed to default
        }

        // Movement
        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveVector = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.Translate(moveVector.normalized * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        // Check if the player is grounded by checking collisions with the ground (modify tag as needed)
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.CompareTag("Ground"))
            {
                isGrounded = true;
                return;
            }
        }
        isGrounded = false;
    }
}

