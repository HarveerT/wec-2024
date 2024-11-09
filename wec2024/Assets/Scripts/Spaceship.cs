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
    [SerializeField] private float _shipSensX = 100.0f;
    [SerializeField] private float _shipSensY = 100.0f;

    /* ==============
     * Private Fields
     * ==============
     */
    private Rigidbody _spaceshipRb;
    private PlayerInput _inputActions;
    private Vector2 _wasdInput;
    private Vector2 _mouseAimInput;

    private void Awake() {
        _inputActions = new PlayerInput();
        _inputActions.Input.Enable();

        // Subscribe to input actions
        _inputActions.Input.WASD.performed += ctx => _wasdInput = ctx.ReadValue<Vector2>();
        _inputActions.Input.WASD.canceled += ctx => _wasdInput = Vector2.zero;

        _inputActions.Input.MouseAim.performed += ctx => _mouseAimInput = ctx.ReadValue<Vector2>();
        _inputActions.Input.MouseAim.canceled += ctx => _mouseAimInput = Vector2.zero;
    }

    void Start() {
        if (_shipVelocityMeterPerSecond <= 0) _shipVelocityMeterPerSecond = 1.0f;
        if (_shipSensX <= 0) _shipSensX = 100.0f;
        if (_shipSensY <= 0) _shipSensY = 100.0f;

        _spaceshipRb = GetComponent<Rigidbody>();
        _spaceshipRb.velocity = transform.forward * _shipVelocityMeterPerSecond;
    }

    void Update() {
        // Determine whether to use mouse or WASD input based on magnitude
        bool takeMouseIn = _wasdInput.normalized.magnitude < _mouseAimInput.normalized.magnitude;

        float xRotation = (takeMouseIn ? _mouseAimInput.y : _wasdInput.y) * _shipSensX * Time.deltaTime;
        float yRotation = (takeMouseIn ? _mouseAimInput.x : _wasdInput.x) * _shipSensY * Time.deltaTime;

        // Apply rotation incrementally
        transform.Rotate(xRotation, yRotation, 0, Space.Self);

        // Clamp rotation values
        Vector3 clampedEulerAngles = new Vector3(
            ClampRotation(transform.eulerAngles.x),
            ClampRotation(transform.eulerAngles.y),
            transform.eulerAngles.z // Keep Z constant or adjust if needed
        );
        transform.eulerAngles = clampedEulerAngles;
    }

    private void FixedUpdate() {
        _spaceshipRb.velocity = transform.forward * _shipVelocityMeterPerSecond;
    }

    float ClampRotation(float angle) {
        angle %= 360f;  // Normalize angle to -360 to 360 range
        if (angle < 0) angle += 360f; // Adjust to 0 to 360
        return angle;
    }
}
