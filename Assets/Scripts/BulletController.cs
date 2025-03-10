using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;  // Destroy bullet after 2 seconds

    void Start()
    {
        Destroy(gameObject, lifetime);  // Auto-destroy after time
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime); // Move forward
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))  // Replace with actual enemy tag
        {
            Destroy(other.gameObject);  // Destroy enemy
            Destroy(gameObject);  // Destroy bullet
        }
    }
}
