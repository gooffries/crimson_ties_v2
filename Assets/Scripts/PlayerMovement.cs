using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    [Header("Movement Settings")]
    public float walkSpeed = 12f;
    public float runSpeed = 18f; // Faster speed when sprinting
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Camera Settings")]
    public Transform playerCamera; // Assign the camera in the Inspector
    public float mouseSensitivity = 100f;

    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f;

    [Header("Spawn Point")]
    public Transform spawnPoint; // Assign this to the desired spawn location

    void Start()
    {
        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Spawn at the designated spawn point if available
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
            Debug.Log($"Player spawned at {spawnPoint.position}");
        }
    }

    void Update()
    {
        HandleMovement();
        HandleCameraLook();
    }

    private void HandleMovement()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        Debug.Log($"Is Grounded: {isGrounded}");

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Detect if the player is holding Shift
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Player movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Jump logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Jump button pressed!");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Apply vertical movement
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleCameraLook()
    {
        // Camera rotation logic
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player left/right
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera up/down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    // Method to transition to a new scene
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Method to save player position (optional)
    public void SavePlayerPosition()
    {
        PlayerPrefs.SetFloat("PlayerX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", transform.position.z);
        Debug.Log("Player position saved.");
    }

    // Method to load player position (optional)
    public void LoadPlayerPosition()
    {
        float x = PlayerPrefs.GetFloat("PlayerX", spawnPoint.position.x);
        float y = PlayerPrefs.GetFloat("PlayerY", spawnPoint.position.y);
        float z = PlayerPrefs.GetFloat("PlayerZ", spawnPoint.position.z);
        transform.position = new Vector3(x, y, z);
        Debug.Log($"Player position loaded: {transform.position}");
    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }
}
