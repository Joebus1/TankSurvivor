using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;  // Adjust speed as needed
    private Transform player;
    private GameManager gameManager;
    private float lastAttackTime; // Track the last attack time
    public float attackCooldown = 1f; // Time between attacks in seconds

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found! Ensure an object is tagged 'Player'.");
        }

        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in scene!");
        }
        lastAttackTime = -attackCooldown; // Initialize to allow immediate first attack
    }

    void Update()
    {
        if (gameManager == null || player == null) return; // Stop if game over or player is destroyed

        // Move the enemy towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        // Get movement direction
        Vector2 direction = (player.position - transform.position).normalized;

        // Calculate the angle (Unity uses Z rotation for 2D)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply rotation to face movement direction
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log($"Enemy {gameObject.name} destroyed by bullet");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Check if enough time has passed since the last attack
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                Debug.Log("Player hit!");
                PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(1); // Deal 1 damage to the player
                }
                lastAttackTime = Time.time; // Update the last attack time
            }
        }
    }
}