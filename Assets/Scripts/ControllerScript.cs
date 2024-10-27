using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] public float walkSpeed = 20.0f; // Speed while walking
    [SerializeField] public float runSpeed = 40.0f;  // Adjusted speed while running
    [SerializeField] public float jumpHeight = 5.0f; // Jump height
    [SerializeField] public float gravity = 20.0f;   // Increased gravity for faster falling
    [SerializeField] public float jumpCooldown = 2.0f; // Cooldown time for jumping

    public Transform cameraTransform; // Reference to the camera's transform
    public Vector3 velocity; // Player's current velocity
    private bool canJump = true; // Flag to track jump availability
    private float jumpTimer = 0f; // Timer for jump cooldown

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        transform.rotation = Quaternion.identity; // Resets the player's rotation
        velocity = Vector3.zero; // Resets any velocity to zero
    }

    private void Update()
    {
        // Movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0; // Ignore the y component for movement
        cameraForward.Normalize();

        // Create the movement direction vector
        Vector3 moveDirection = (cameraForward * moveZ + cameraTransform.right * moveX).normalized;

        // Determine current speed: Only run if there is movement input
        float currentSpeed = (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift)) ? runSpeed : walkSpeed;

        // Calculate horizontal movement
        Vector3 move = moveDirection * currentSpeed * Time.deltaTime;

        // Apply horizontal movement
        controller.Move(move); // Apply the movement

        // Rotate player towards movement direction for A and D
        if (moveX != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 720 * Time.deltaTime);
        }

        // Jump input with cooldown
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity); // Calculate jump velocity
            canJump = false; // Set cooldown
            jumpTimer = jumpCooldown; // Reset timer
        }

        // Apply gravity
        velocity.y -= gravity * Time.deltaTime; // Apply downward force

        // Move the character based on the vertical velocity, maintaining horizontal speed
        controller.Move(new Vector3(move.x, velocity.y * Time.deltaTime, move.z));

        // Cooldown management
        if (!canJump)
        {
            jumpTimer -= Time.deltaTime; // Decrease timer
            if (jumpTimer <= 0)
            {
                canJump = true; // Reset jump availability
            }
        }

        // Check if the player has landed
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Prevent sticking to the ground
        }
    }
}
