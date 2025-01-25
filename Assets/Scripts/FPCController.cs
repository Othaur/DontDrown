using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FluidDynamicsController : MonoBehaviour
{
    // Movement settings
    public float forwardSpeed = 70.0f; // Speed when swimming forward (10x increase)
    public float lateralSpeed = 40.0f; // Speed for lateral (left/right) swimming (10x increase)
    public float sinkingSpeed = 3.0f; // Speed for sinking when idle
    public float dragCoefficient = 0.85f; // General drag applied to movement for smoother momentum

    // Buoyancy and gravity
    public float buoyancy = 2.0f; // Upward force counteracting gravity while swimming
    public float gravity = -9.8f; // Downward pull (adjusted for water)
    public float upwardSpeed = 29.4f; // Adjusted to match downward speed when sinking
    public float strokeCycleTime = 1.0f; // Time for a full stroke cycle

    // Water flow settings
    public Vector3 constantFlow = new Vector3(1.0f, 0, 0); // Constant water flow direction and speed

    // Internal variables
    private CharacterController characterController;
    private Vector3 velocity = Vector3.zero; // Current velocity of the swimmer
    private float strokeTimer = 0.0f; // Timer to track stroke cycles
    private float bottomOscillation;
    private float topOscillation;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock the cursor for better camera control
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Set initial oscillation bounds
        SetNewOscillationBounds();
    }

    void Update()
    {
        // Handle camera rotation
        HandleCameraRotation();

        // Process swimming movement based on input
        Vector3 inputDirection = GetInputDirection();

        if (inputDirection != Vector3.zero) // Only apply stroke motion when WASD keys are pressed
        {
            // Update stroke cycle
            strokeTimer += Time.deltaTime;
            if (strokeTimer > strokeCycleTime)
            {
                strokeTimer -= strokeCycleTime;
                SetNewOscillationBounds(); // Update bounds for the next cycle
            }

            // Add oscillating vertical stroke motion within the bounds
            float strokePhase = (strokeTimer / strokeCycleTime) * 2 * Mathf.PI; // Convert to radians
            float strokeOffset = Mathf.Sin(strokePhase); // Oscillates between -1 and 1
            float oscillation = Mathf.Lerp(bottomOscillation, topOscillation, (strokeOffset + 1) / 2); // Map to bounds

            velocity.y = Mathf.Lerp(velocity.y, oscillation, Time.deltaTime * 2);
        }

        // Simulate frog-like breaststroke dynamics for directional movement
        if (inputDirection.x != 0) // Left/Right movement (widespread stroke)
        {
            velocity.x = Mathf.Lerp(velocity.x, inputDirection.x * lateralSpeed, Time.deltaTime * 3);
        }

        if (inputDirection.z != 0) // Forward/Backward movement (streamlined stroke)
        {
            velocity.z = Mathf.Lerp(velocity.z, inputDirection.z * forwardSpeed, Time.deltaTime * 3);
        }

        // Handle manual Y-axis movement (up/down)
        if (Input.GetKey(KeyCode.Space)) // Move up
        {
            velocity.y = Mathf.Lerp(velocity.y, upwardSpeed, Time.deltaTime * 2); // Add drag to upward movement
        }
        else if (Input.GetKey(KeyCode.LeftShift)) // Move down
        {
            velocity.y = Mathf.Lerp(velocity.y, gravity * sinkingSpeed, Time.deltaTime * 2);
        }

        // Apply sinking when no movement input is detected
        if (inputDirection == Vector3.zero && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift))
        {
            velocity.y = Mathf.Lerp(velocity.y, gravity * sinkingSpeed, Time.deltaTime);
        }

        // Apply drag to smooth momentum changes and mimic fluid resistance
        velocity *= dragCoefficient;

        // Add constant water flow to movement for dynamic immersion
        velocity += constantFlow * Time.deltaTime;

        // Move the character
        characterController.Move(velocity * Time.deltaTime);
    }

    private void HandleCameraRotation()
    {
        float lookSpeed = 2.0f;
        float lookXLimit = 45.0f;

        // Rotate the camera up/down
        float rotationX = -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        // Rotate the player left/right
        float rotationY = Input.GetAxis("Mouse X") * lookSpeed;
        transform.rotation *= Quaternion.Euler(0, rotationY, 0);
    }

    private Vector3 GetInputDirection()
    {
        // Get movement input (WASD or arrow keys)
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float moveVertical = Input.GetAxis("Vertical");   // W/S or Up/Down

        // Combine input into a direction vector
        Vector3 direction = new Vector3(moveHorizontal, 0, moveVertical).normalized;
        return direction;
    }

    private void SetNewOscillationBounds()
    {
        bottomOscillation = Random.Range(-2.25f, -1.5f);
        topOscillation = Random.Range(1.6f, 2.3f);
    }
}
