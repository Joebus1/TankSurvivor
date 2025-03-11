using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;  // Adjust speed as needed
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
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
            Time.timeScale = 0f;  // Pauses the game
        }
    }
}