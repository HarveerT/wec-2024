using UnityEngine;
using static PlayerInput;
using UnityEngine.InputSystem;

public class Spaceship : MonoBehaviour {
    /* =================
     * Serialized Fields
     * =================
     */

    [SerializeField] private float _shipVelocityMeterPerSecond = 1.0f; // ship speed in m/s

    // Turning sensitivities
    [SerializeField] private float _shipRollSens = 100.0f;
    [SerializeField] private float _shipPitchSens = 100.0f;
    [SerializeField] private float _shipYawSens = 100.0f;

    /* ============== 
     * Private Fields 
     * ============== 
     */
    private Rigidbody _spaceshipRb;
    private PlayerInput _inputActions;

    private float _yawInput;
    private float _pitchInput;
    private float _rollInput;

    private void Awake() {
        _inputActions = new PlayerInput();
        _inputActions.Input.Enable();

        // Subscribe to input actions
        _inputActions.Input.Yaw.performed += ctx => _yawInput = ctx.ReadValue<float>();
        _inputActions.Input.Yaw.canceled += ctx => _yawInput = 0.0f;

        _inputActions.Input.Roll.performed += ctx => _rollInput = ctx.ReadValue<float>();
        _inputActions.Input.Roll.canceled += ctx => _rollInput = 0.0f;

        _inputActions.Input.Pitch.performed += ctx => _pitchInput = ctx.ReadValue<float>();
        _inputActions.Input.Pitch.canceled += ctx => _pitchInput = 0.0f;
    }

    void Start() {
        _spaceshipRb = GetComponent<Rigidbody>();
        _spaceshipRb.linearVelocity = transform.forward * _shipVelocityMeterPerSecond;
        _spaceshipRb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update() {
        // Apply turning logic with proper sensitivities and Time.deltaTime for frame-rate independence
        float yaw = _yawInput * _shipYawSens * Time.deltaTime;
        float roll = _rollInput * _shipRollSens * Time.deltaTime;
        float pitch = _pitchInput * _shipPitchSens * Time.deltaTime;

        // Apply rotation incrementally in local space
        transform.Rotate(Vector3.right * pitch, Space.Self);  // Pitch around local X axis
        transform.Rotate(Vector3.up * yaw, Space.Self);     // Yaw around local Y axis
        transform.Rotate(Vector3.forward * roll, Space.Self); // Roll around local Z axis
    }

    private void FixedUpdate() {
        // Update the spaceship's forward velocity
        _spaceshipRb.linearVelocity = transform.forward * _shipVelocityMeterPerSecond;
    }

    // Function to clamp rotation between 0 and 360 degrees
    float ClampRotation(float angle) {
        angle %= 360f;  // Normalize angle to -360 to 360 range
        if (angle < 0) angle += 360f; // Adjust to 0 to 360
        return angle;
    }
}
