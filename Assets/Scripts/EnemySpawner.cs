using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public Transform player; // Reference to the player's transform
    public float spawnRate = 2f; // Time between spawns in seconds
    public int mapWidth = 16; // Match this with BorderManager (playable area width)
    public int mapHeight = 16; // Match this with BorderManager (playable area height)
    public float borderThickness = 1f; // Thickness of the border (to spawn inside)
    public float minSpawnDistance = 5f; // Minimum distance from player to spawn
    public float maxSpawnDistance = 10f; // Maximum distance from player to spawn

    private float nextSpawnTime;

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        // Calculate the playable area bounds (inside the border)
        float minX = -mapWidth / 2 + borderThickness;
        float maxX = mapWidth / 2 - borderThickness;
        float minY = -mapHeight / 2 + borderThickness;
        float maxY = mapHeight / 2 - borderThickness;

        Vector3 spawnPosition = Vector3.zero; // Initialize to avoid unassigned variable error
        bool validPosition = false;
        int maxAttempts = 10; // Prevent infinite loops

        // Keep trying to find a valid spawn position
        for (int i = 0; i < maxAttempts; i++)
        {
            // Randomly choose a position within the playable area
            float spawnX = Random.Range(minX, maxX);
            float spawnY = Random.Range(minY, maxY);
            spawnPosition = new Vector3(spawnX, spawnY, 0);

            // Calculate the direction from player to spawn position
            Vector3 directionToSpawn = (spawnPosition - player.position).normalized;

            // Prefer spawning on the opposite side by adjusting the position
            // Move the spawn point further in the opposite direction of the player
            Vector3 oppositeDirection = -player.position.normalized;
            spawnPosition = player.position + oppositeDirection * Random.Range(minSpawnDistance, maxSpawnDistance);

            // Clamp the position to stay within the playable area
            spawnPosition.x = Mathf.Clamp(spawnPosition.x, minX, maxX);
            spawnPosition.y = Mathf.Clamp(spawnPosition.y, minY, maxY);

            // Check if the spawn position is within the min/max distance from the player
            float distanceToPlayer = Vector3.Distance(spawnPosition, player.position);
            if (distanceToPlayer >= minSpawnDistance && distanceToPlayer <= maxSpawnDistance)
            {
                validPosition = true;
                break;
            }
        }

        if (!validPosition)
        {
            // Fallback: Spawn at a random position inside the bounds if no valid position found
            spawnPosition = new Vector3(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY),
                0
            );
        }

        // Spawn the enemy
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        Debug.Log($"Enemy spawned at: {spawnPosition}");
    }
}