using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;  // Adjust speed as needed
    private Transform player;
    private GameManager gameManager;

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

        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in scene!");
        }
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
            Debug.Log("Player hit! Game Over!");
            Destroy(collision.gameObject);  // Destroys the player
            if (gameManager != null)
            {
                gameManager.GameOver();  // Notify GameManager of game over
            }
        }
    }
}