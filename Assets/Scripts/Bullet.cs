using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;  // Destroy bullet after 2 seconds
    private bool hasHit = false; // Prevent multiple hits
    private Rigidbody2D rb;      // Cached Rigidbody2D

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // Prevent tunneling
            rb.bodyType = RigidbodyType2D.Dynamic; // Ensure dynamic
        }
        else
        {
            Debug.LogError("Bullet missing Rigidbody2D!");
        }
        StartCoroutine(DestroyAfterLifetime());
        Debug.Log($"Bullet {gameObject.name} started with {lifetime} second lifetime");
    }

    void Update()
    {
        if (rb != null && !hasHit)
        {
            rb.linearVelocity = transform.up * speed; // Use velocity for physics movement
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Bullet collided with: {collision.gameObject.name}, Tag: {collision.gameObject.tag}, Layer: {LayerMask.LayerToName(collision.gameObject.layer)}");
        if (hasHit) return; // Prevent multiple hits

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Border"))
        {
            hasHit = true;
            Destroy(gameObject);  // Destroy bullet on hit
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Debug.Log($"Bullet hit enemy: {collision.gameObject.name}");
                Destroy(collision.gameObject);  // Destroy enemy
            }
            else if (collision.gameObject.CompareTag("Border"))
            {
                Debug.Log("Bullet hit border");
            }
        }
    }

    private IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        if (!hasHit && gameObject != null) // Only destroy if it hasn't hit and still exists
        {
            Debug.Log($"Bullet {gameObject.name} lifetime expired");
            Destroy(gameObject);
        }
    }
}