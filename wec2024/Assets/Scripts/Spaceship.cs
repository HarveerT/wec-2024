using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlaneController : MonoBehaviour {
    [SerializeField] private float pitchSensitivity = 2f;
    [SerializeField] private float yawSensitivity = 2f;
    [SerializeField] private float rollFactor = 0.5f;
    [SerializeField] private float forwardSpeed = 20f;  // Forward speed of the plane

    private Rigidbody rb;
    private Vector2 mouseDelta;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;           // Disable gravity to prevent unintended fall
        rb.freezeRotation = true;        // Control rotation manually to prevent physics interference

        // Hide and lock the cursor to make mouse movement relative
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        // Capture relative mouse input for pitch and yaw
        mouseDelta = Mouse.current.delta.ReadValue();

        // Calculate pitch and yaw from mouse input
        float pitch = -mouseDelta.y * pitchSensitivity * Time.deltaTime;
        float yaw = mouseDelta.x * yawSensitivity * Time.deltaTime;

        // Calculate roll based on yaw to make the turn smooth
        float roll = -yaw * rollFactor;

        // Apply rotation to the plane
        transform.Rotate(pitch, yaw, roll);
    }

    private void FixedUpdate() {
        // Move the plane forward at a constant speed in the forward (z-axis) direction
        rb.linearVelocity = transform.forward * forwardSpeed;
    }
}
