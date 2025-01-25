using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FluidDynamicsController : MonoBehaviour
{
    // Physics constants
    public float forwardSpeed = 35.0f; // Reduced horizontal movement speed (50% shorter)
    public float lateralSpeed = 20.0f; // Reduced side-to-side movement speed (50% shorter)
    public float sinkingSpeed = 20.0f; // Default sinking speed
    public float dragCoefficient = 0.98f; // Increased drag for faster stopping
    public float buoyancyForce = 30.0f; // Upward force counteracting sinking
    public float gravity = -9.8f; // Simulated gravity in water

    // Oscillation constants
    public float strokeCycleTime = 1.0f; // Time for a full oscillation cycle
    public float oscillationAmplitude = 2.0f; // Amplitude of the oscillation

    // Water flow settings
    public Vector3 waterFlow = new Vector3(5.0f, 0, 0); // Constant water flow direction

    // Camera settings
    public float verticalLookSpeed = 2.0f; // Speed of vertical camera look
    public float maxVerticalAngle = 45.0f; // Maximum camera pitch angle

    private CharacterController characterController;
    private Vector3 velocity = Vector3.zero; // Player velocity
    private float strokeTimer = 0.0f; // Timer to track oscillation phase
    private float initialYPosition = 0.0f; // Initial Y position to ensure oscillation starts and ends consistently
    private float currentCameraPitch = 0.0f; // Tracks camera pitch for vertical look

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock the cursor for better control
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Store the initial Y position
        initialYPosition = transform.position.y;
    }

    void Update()
    {
        ApplyOscillation();         // Constant up-and-down movement
        ApplyHorizontalMovement();  // WASD controls for X/Z movement
        ApplySpacebarControls();    // Spacebar to tread water

        ApplySinking();             // Apply sinking behavior
        ApplyBuoyancy();            // Apply buoyancy force

        // Apply final movement and forces
        characterController.Move(velocity * Time.deltaTime);
        Debug.Log($"Final velocity: {velocity}");
    }

    private void ApplyOscillation()
    {
        // Increment the oscillation timer
        strokeTimer += Time.deltaTime;
        if (strokeTimer > strokeCycleTime)
        {
            strokeTimer -= strokeCycleTime;
            Debug.Log("Oscillation cycle reset. Timer restarted.");
        }

        // Calculate the oscillation phase (normalized to 0-1)
        float oscillationPhase = strokeTimer / strokeCycleTime;

        // Calculate the oscillation offset using a sine wave
        float oscillation = Mathf.Sin(oscillationPhase * Mathf.PI * 2) * oscillationAmplitude;

        // Apply the oscillation directly to the Y position
        float newYPosition = initialYPosition + oscillation;
        velocity.y = (newYPosition - transform.position.y) / Time.deltaTime;

        Debug.Log($"Oscillation active: Phase={oscillationPhase:F2}, Value={oscillation:F2}, New Y Position={newYPosition:F2}");
    }

    private void ApplyHorizontalMovement()
    {
        // Get input direction
        Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        // Apply movement input to velocity
        if (inputDirection != Vector3.zero)
        {
            velocity.x = Mathf.Lerp(velocity.x, inputDirection.x * lateralSpeed, Time.deltaTime * 3);
            velocity.z = Mathf.Lerp(velocity.z, inputDirection.z * forwardSpeed, Time.deltaTime * 3);
            Debug.Log($"Horizontal input detected: {inputDirection}");
        }
        else
        {
            // Apply drag to quickly stop horizontal movement
            velocity.x *= dragCoefficient;
            velocity.z *= dragCoefficient;
            Debug.Log("Applying drag to horizontal movement.");
        }
    }

    private void ApplySpacebarControls()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Stop horizontal movement
            velocity.x = Mathf.Lerp(velocity.x, 0, Time.deltaTime * 5);
            velocity.z = Mathf.Lerp(velocity.z, 0, Time.deltaTime * 5);

            // Maintain vertical oscillation
            Debug.Log("Treading water. Horizontal movement stopped.");
        }
    }

    private void ApplySinking()
    {
        // Apply sinking when no input and no spacebar is pressed
        if (!Input.GetKey(KeyCode.Space) && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.1f && Mathf.Abs(Input.GetAxis("Vertical")) < 0.1f)
        {
            velocity.y -= sinkingSpeed * Time.deltaTime;
            Debug.Log("Player sinking due to no input.");
        }
        else
        {
            Debug.Log("Sinking not applied (input detected or spacebar pressed).");
        }
    }

    private void ApplyBuoyancy()
    {
        // Apply buoyancy force to counteract sinking
        if (velocity.y < 0)
        {
            velocity.y += buoyancyForce * Time.deltaTime;
            Debug.Log($"Buoyancy applied. Vertical velocity: {velocity.y}");
        }
    }

    private void HandleCameraRotation()
    {
        float lookX = -Input.GetAxis("Mouse Y") * verticalLookSpeed;
        currentCameraPitch = Mathf.Clamp(currentCameraPitch + lookX, -maxVerticalAngle, maxVerticalAngle);
        Camera.main.transform.localRotation = Quaternion.Euler(currentCameraPitch, 0, 0);

        float lookY = Input.GetAxis("Mouse X") * verticalLookSpeed;
        transform.Rotate(0, lookY, 0);

        Debug.Log($"Camera rotation updated. Pitch: {currentCameraPitch}, Yaw: {transform.rotation.eulerAngles.y}");
    }
}
