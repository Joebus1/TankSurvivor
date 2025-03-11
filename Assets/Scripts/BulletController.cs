using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;  // Destroy bullet after 2 seconds
    private bool hasHit = false; // Prevent multiple hits

    void Start()
    {
        Destroy(gameObject, lifetime);  // Auto-destroy after time
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime); // Move forward
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasHit) return; // Prevent multiple hits

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log($"Bullet hit enemy: {collision.gameObject.name}");
            hasHit = true;
            Destroy(collision.gameObject);  // Destroy enemy
            Destroy(gameObject);  // Destroy bullet
        }
        else if (collision.gameObject.CompareTag("Border"))
        {
            hasHit = true;
            Destroy(gameObject);  // Destroy bullet on border hit
        }
    }
}