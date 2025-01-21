using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody; // Reference to the player's body (for yaw)

    float xRotation = 0f;

    void Start()
    {
        // Lock the cursor to the middle of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the camera up and down (pitch)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent over-rotation
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player left and right (yaw)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
