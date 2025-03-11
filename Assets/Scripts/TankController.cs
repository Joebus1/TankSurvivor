using UnityEngine;

public class TankController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("TankController requires a Rigidbody2D!");
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void FixedUpdate() // Use FixedUpdate for physics
    {
        float moveInput = Input.GetAxis("Vertical"); // W/S or Up/Down arrows
        float rotateInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows

        // Rotate the tank
        float rotation = -rotateInput * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotation);

        // Move forward or backward
        Vector2 movement = transform.up * -moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }
}