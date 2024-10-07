using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] public float walkSpeed = 20.0f; // Speed while walking
    [SerializeField] public float runSpeed = 40.0f;  // Speed while running
    [SerializeField] public float gravity = 9.81f;   // Gravity applied to the player

    public Transform cameraTransform; // Reference to the camera's transform
    private Vector3 velocity; // Player's current velocity
    public bool isGrounded; // Boolean to check if the player is grounded

    public float groundCheckDistance = 0.1f; // Distance to check for ground
    public LayerMask groundLayer; // Layer to check against for ground

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        transform.rotation = Quaternion.identity; // Resets the player's rotation
        velocity = Vector3.zero; // Resets any velocity to zero
    }

    private void Update()
    {
        // Check if the player is grounded using a sphere cast
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundLayer);

        // Reset vertical velocity if grounded
        if (isGrounded)
        {
            if (velocity.y < 0)
            {
                velocity.y = -2f; // Prevents sticking to the ground
            }
        }

        // Movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0; // Ignore the y component for movement
        cameraForward.Normalize();

        // Create the movement direction vector
        Vector3 moveDirection = (cameraForward * moveZ + cameraTransform.right * moveX).normalized;

        // Determine current speed: Only run if there is movement input
        float currentSpeed = (moveDirection != Vector3.zero) && Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Calculate horizontal movement
        Vector3 move = moveDirection * currentSpeed * Time.deltaTime;

        // Apply horizontal movement while keeping Y position fixed
        Vector3 targetPosition = transform.position + new Vector3(move.x, 0, move.z); // Only change X and Z
        controller.Move(targetPosition - transform.position); // Apply the movement

        // Rotate player towards movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 720 * Time.deltaTime);
        }

        // Apply gravity
        if (!isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime; // Apply downward force if needed
        }
        else
        {
            velocity.y = -2f; // Reset velocity if grounded to prevent floating
        }

        // Move the character based on the vertical velocity
        controller.Move(velocity * Time.deltaTime);
    }
}
