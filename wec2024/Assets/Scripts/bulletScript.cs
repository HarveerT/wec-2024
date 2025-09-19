using UnityEngine;

public class bulletScript : MonoBehaviour
{
    [SerializeField] private float speed = 100;
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = transform.up * speed;
    }
}
