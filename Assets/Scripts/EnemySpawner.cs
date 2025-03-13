using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public Transform player; // Reference to the player's transform
    public float spawnRate = 2f; // Time between spawns in seconds
    public float minSpawnDistance = 5f; // Minimum distance from player to spawn
    public float maxSpawnDistance = 10f; // Maximum distance from player to spawn

    private float nextSpawnTime;
    private MapSettings mapSettings; // Reference to MapSettings

    private void Start()
    {
        // Find the MapSettings instance
        mapSettings = MapSettings.Instance;
        if (mapSettings == null)
        {
            Debug.LogError("MapSettings not found in scene!");
        }
    }

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
        if (mapSettings == null) return; // Safety check

        // Get map size and border thickness from MapSettings
        float mapWidth = mapSettings.GetMapWidth();
        float mapHeight = mapSettings.GetMapHeight();
        float borderThickness = mapSettings.GetBorderThickness();

        // Calculate the outer bounds (outside the border)
        float outerMinX = -mapWidth / 2 - borderThickness;
        float outerMaxX = mapWidth / 2 + borderThickness;
        float outerMinY = -mapHeight / 2 - borderThickness;
        float outerMaxY = mapHeight / 2 + borderThickness;

        Vector3 spawnPosition = Vector3.zero;
        bool validPosition = false;
        int maxAttempts = 10; // Prevent infinite loops

        // Keep trying to find a valid spawn position outside the border
        for (int i = 0; i < maxAttempts; i++)
        {
            // Choose a random side to spawn outside (0: left, 1: right, 2: bottom, 3: top)
            int side = Random.Range(0, 4);
            switch (side)
            {
                case 0: // Left side (outside)
                    spawnPosition = new Vector3(outerMinX - 1, Random.Range(outerMinY, outerMaxY), 0);
                    break;
                case 1: // Right side (outside)
                    spawnPosition = new Vector3(outerMaxX + 1, Random.Range(outerMinY, outerMaxY), 0);
                    break;
                case 2: // Bottom side (outside)
                    spawnPosition = new Vector3(Random.Range(outerMinX, outerMaxX), outerMinY - 1, 0);
                    break;
                case 3: // Top side (outside)
                    spawnPosition = new Vector3(Random.Range(outerMinX, outerMaxX), outerMaxY + 1, 0);
                    break;
            }

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
            // Fallback: Choose a random side and spawn outside without distance check
            int side = Random.Range(0, 4);
            switch (side)
            {
                case 0: // Left side (outside)
                    spawnPosition = new Vector3(outerMinX - 1, Random.Range(outerMinY, outerMaxY), 0);
                    break;
                case 1: // Right side (outside)
                    spawnPosition = new Vector3(outerMaxX + 1, Random.Range(outerMinY, outerMaxY), 0);
                    break;
                case 2: // Bottom side (outside)
                    spawnPosition = new Vector3(Random.Range(outerMinX, outerMaxX), outerMinY - 1, 0);
                    break;
                case 3: // Top side (outside)
                    spawnPosition = new Vector3(Random.Range(outerMinX, outerMaxX), outerMaxY + 1, 0);
                    break;
            }
        }

        // Spawn the enemy
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        Debug.Log($"Enemy spawned at: {spawnPosition}");
    }
}