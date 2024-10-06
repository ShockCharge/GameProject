using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    CharacterController controller;

    [SerializeField] public float walkSpeed = 5.0f;
    [SerializeField] public float runSpeed = 10.0f;
    [SerializeField] public float jumpHeight = 2.0f;
    [SerializeField] public float gravity = -9.81f;

    private Vector3 velocity;
    public bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0) { velocity.y = -2f; }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Determine speed (running or walking)
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Apply movement to the CharacterController
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Jumping logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity over time
        velocity.y += gravity * Time.deltaTime;

        // Move the character based on gravity and vertical velocity
        controller.Move(velocity * Time.deltaTime);
    }
}
