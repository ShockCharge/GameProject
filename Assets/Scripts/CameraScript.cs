using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    [SerializeField] public float distance = 3.0f;  // Distance the camera stays behind the player
    [SerializeField] public float height = 1.5f;    // Height of the camera above the player
    [SerializeField] public float rotationSpeed = 1.0f;  // Speed of camera rotation around the player

    private float currentX = 0.0f;  // Rotation on the X-axis
    private float currentY = 0.0f;  // Rotation on the Y-axis

    [SerializeField] public float sensitivityX = 4.0f;  // Sensitivity for mouse X movement
    [SerializeField] public float sensitivityY = 2.0f;  // Sensitivity for mouse Y movement
    [SerializeField] public float minYAngle = -20.0f;   // Minimum vertical angle
    [SerializeField] public float maxYAngle = 80.0f;    // Maximum vertical angle

    void Update()
    {
        // Get mouse input for rotating the camera
        currentX += Input.GetAxis("Mouse X") * sensitivityX;
        currentY -= Input.GetAxis("Mouse Y") * sensitivityY;

        // Clamp the vertical rotation to avoid flipping
        currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);
    }

    void LateUpdate()
    {
        // Ensure the player transform is assigned
        if (player != null)
        {
            // Calculate the new camera position based on rotation
            Vector3 direction = new Vector3(0, height, -distance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            transform.position = player.position + rotation * direction;

            // Make the camera look at the player
            transform.LookAt(player.position + Vector3.up * height);
        }
    }
}
