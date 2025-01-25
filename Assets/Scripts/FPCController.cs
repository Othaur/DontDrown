using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class FPCController : MonoBehaviour
{
    // Movement settings
    public float burstSpeed = 7.0f; // Speed during the initial burst
    public float baseSwimSpeed = 3.0f; // Regular swim speed after the burst
    public float drag = 1.0f; // Deceleration factor after the burst
    public float sinkSpeed = 15.0f; // Speed at which the player sinks

    // Swim timing
    public float burstDuration = 0.5f; // Duration of the initial burst
    public float swimDuration = 1.5f; // Time spent swimming after the burst
    public float sinkDuration = 0.25f; // Time spent sinking between strokes

    // Camera and rotation settings
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    // Internal variables
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private float swimTimer = 0; // Tracks swimming time
    private bool isSwimming = false; // Whether the player is swimming
    private bool isSinking = false; // Whether the player is sinking

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Handle camera rotation
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        // Determine if the player is moving
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        if (isMoving)
        {
            HandleSwimming();
        }
        else
        {
            HandleSinking();
        }

        // Apply movement to the CharacterController
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void HandleSwimming()
    {
        if (!isSwimming) // Start swimming
        {
            isSwimming = true;
            swimTimer = burstDuration + swimDuration; // Reset swim timer
            isSinking = false; // Stop sinking
        }

        swimTimer -= Time.deltaTime;

        if (swimTimer > swimDuration) // Burst phase
        {
            moveDirection = GetMovementDirection(burstSpeed);
        }
        else if (swimTimer > 0) // Regular swim phase
        {
            moveDirection = Vector3.Lerp(moveDirection, GetMovementDirection(baseSwimSpeed), drag * Time.deltaTime);
        }
        else // Transition to sinking phase
        {
            isSwimming = false;
            isSinking = true;
        }
    }

    void HandleSinking()
    {
        if (isSinking || !isSwimming)
        {
            // Apply sinking speed directly to the Y-axis
            moveDirection.y = -sinkSpeed * Time.deltaTime;
        }
        else
        {
            // Reset movement if fully idle
            moveDirection = Vector3.zero;
        }
    }

    Vector3 GetMovementDirection(float speed)
    {
        // Combine forward/backward and left/right directions based on input
        Vector3 forward = transform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical");
        Vector3 right = transform.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal");
        return (forward + right).normalized * speed; // Normalize to ensure consistent speed
    }
}
