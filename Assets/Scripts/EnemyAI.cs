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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit! Game Over!");
            Destroy(other.gameObject);  // Destroys the player
            Time.timeScale = 0f;  // Pauses the game
        }
    }
}