using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPCController : MonoBehaviour
{
    // Physics constants
    public float forwardSpeed = 15.5f; // Reduced horizontal movement speed (50% shorter) 
    public float lateralSpeed = 7.0f; // Reduced side-to-side movement speed (50% shorter)
    public float sinkingSpeed = 7.0f; // Default sinking speed (reduced to match upward movement)
    public float dragCoefficient = 0.99f; // Increased drag for faster stopping
    public float buoyancyForce = 30.0f; // Upward force counteracting sinking
    public float gravity = -9.8f; // Simulated gravity in water

    // Oscillation constants
    public float strokeCycleTime = 1.25f; // Time for a full oscillation cycle
    public float oscillationAmplitude = 1.6f; // Amplitude of the oscillation

    // Water flow settings
    public Vector3 waterFlow = new Vector3(2.0f, 0, 0); // Reduced constant water flow direction

    // Camera settings
    public float verticalLookSpeed = 50f; // Increased speed of vertical camera look
    public float horizontalLookSpeed = 50f; // Increased speed of horizontal camera look
    public float maxVerticalAngle = 67.5f; // Maximum camera pitch angle

    private CharacterController characterController;
    private Vector3 velocity = Vector3.zero; // Player velocity
    private float strokeTimer = 0.0f; // Timer to track oscillation phase
    private float currentOscillationBase = 0.0f; // Current Y base for oscillation
    private float currentCameraPitch = 0.0f; // Tracks camera pitch for vertical look
    private bool updateOscillationBase = true; // Flag to control oscillation base updates
    private bool isRisingWithOscillation = false; // Flag for diagonal rising oscillation

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock the cursor for better control
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initialize the oscillation base
        currentOscillationBase = transform.position.y;
    }

    void Update()
    {
        Debug.Log($"Mouse Input Test: X = {Input.GetAxis("Mouse X")}, Y = {Input.GetAxis("Mouse Y")}");
        HandleCameraRotation();    // Adjust camera rotation first
        ApplyOscillation();         // Constant up-and-down movement
        ApplyHorizontalMovement();  // WASD controls for X/Z movement
        ApplySpacebarControls();    // Spacebar to rise
        ApplySinking();             // Apply sinking behavior when idle

        // Apply final movement and forces
        characterController.Move(velocity * Time.deltaTime);
    }

    private void ApplyOscillation()
    {
        // Increment the oscillation timer
        strokeTimer += Time.deltaTime;
        if (strokeTimer > strokeCycleTime)
        {
            strokeTimer -= strokeCycleTime;
        }

        // Calculate the oscillation phase (normalized to 0-1)
        float oscillationPhase = strokeTimer / strokeCycleTime;

        // Calculate the oscillation offset using a sine wave
        float oscillation = Mathf.Sin(oscillationPhase * Mathf.PI * 2) * oscillationAmplitude;

        // Adjust oscillation for rising diagonally
        if (isRisingWithOscillation)
        {
            float predictedRiseOffset = velocity.y * strokeCycleTime * (1 - oscillationPhase); // Predict rise continuation
            oscillation += predictedRiseOffset; // Diagonal rise adjustment with prediction
        }

        // Smoothly update the oscillation base
        if (updateOscillationBase)
        {
            currentOscillationBase = Mathf.Lerp(currentOscillationBase, transform.position.y, Time.deltaTime * 3); // Smooth transition
            updateOscillationBase = false;
        }

        float newYPosition = currentOscillationBase + oscillation;
        velocity.y = (newYPosition - transform.position.y) / Time.deltaTime;
    }

    private void ApplyHorizontalMovement()
    {
        // Get input direction
        Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        if (inputDirection != Vector3.zero)
        {
            // Calculate movement relative to the player's orientation
            Vector3 moveDirection = (transform.right * inputDirection.x + transform.forward * inputDirection.z).normalized;

            // Apply movement to velocity
            velocity.x = Mathf.Lerp(velocity.x, moveDirection.x * lateralSpeed, Time.deltaTime * 3);
            velocity.z = Mathf.Lerp(velocity.z, moveDirection.z * forwardSpeed, Time.deltaTime * 3);
        }
        else
        {
            // Apply drag to quickly stop horizontal movement
            velocity.x *= dragCoefficient;
            velocity.z *= dragCoefficient;
        }
    }

    private void ApplySpacebarControls()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Rise upward at the same speed as sinking
            velocity.y = sinkingSpeed;
            isRisingWithOscillation = true; // Enable diagonal rising oscillation
            updateOscillationBase = true; // Allow oscillation base to update after rising
        }
        else
        {
            isRisingWithOscillation = false; // Disable diagonal rising oscillation when spacebar is released
        }
    }

    private void ApplySinking()
    {
        // Apply sinking when no input and no spacebar is pressed
        if (!Input.GetKey(KeyCode.Space) && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.1f && Mathf.Abs(Input.GetAxis("Vertical")) < 0.1f)
        {
            velocity.y = -sinkingSpeed;
            updateOscillationBase = true; // Allow oscillation base to update after sinking
        }
    }

    private void HandleCameraRotation()
    {
        // Adjust vertical camera rotation (pitch)
        float lookX = -Input.GetAxis("Mouse Y") * verticalLookSpeed;
        currentCameraPitch = Mathf.Clamp(currentCameraPitch + lookX, -maxVerticalAngle, maxVerticalAngle);

        // Apply the vertical rotation
        Camera.main.transform.localRotation = Quaternion.Euler(currentCameraPitch, 0, 0);

        // Adjust horizontal player rotation (yaw) using Quaternion
        float lookY = Input.GetAxis("Mouse X") * horizontalLookSpeed;
        Quaternion horizontalRotation = Quaternion.Euler(0, lookY, 0);
        transform.rotation *= horizontalRotation; // Accumulate yaw rotation
    }

}
