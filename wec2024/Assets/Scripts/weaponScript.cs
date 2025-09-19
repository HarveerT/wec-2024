using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class weaponScript : MonoBehaviour {
    // Set the number of times the function is called per minute
    [SerializeField] private int rpm = 10;
    private float roundInterval;
    private float lastFireTime;


    [SerializeField] private Transform bulletSpawnLeft;
    [SerializeField] private Transform bulletSpawnRight;
    [SerializeField] private GameObject bullet;

    private void Start() {
        roundInterval = 60f / rpm;
        lastFireTime = -roundInterval;  // Ensure it can call immediately if button is held
    }

    private void Update() {
        // Check if the left mouse button is held down
        if (Input.GetMouseButton(0)) {
            // Only call the function if the time interval has passed
            if (Time.time - lastFireTime >= roundInterval) {
                CallFunction();
                lastFireTime = Time.time;  // Reset the last call time
            }
        }
    }

    private void CallFunction() {
        Instantiate(bullet, bulletSpawnRight.position, bulletSpawnRight.rotation);
        Instantiate(bullet, bulletSpawnLeft.position, bulletSpawnLeft.rotation);
    }
}
