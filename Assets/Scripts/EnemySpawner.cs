using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public float spawnRate = 2f;
    public int mapWidth = 16; // Match this with BorderManager
    public int mapHeight = 16;

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
        Vector3 spawnPosition;
        int side = Random.Range(0, 4); // Choose random side

        switch (side)
        {
            case 0: // Left side
                spawnPosition = new Vector3(-mapWidth / 2 - 1, Random.Range(-mapHeight / 2, mapHeight / 2), 0);
                break;
            case 1: // Right side
                spawnPosition = new Vector3(mapWidth / 2 + 1, Random.Range(-mapHeight / 2, mapHeight / 2), 0);
                break;
            case 2: // Bottom side
                spawnPosition = new Vector3(Random.Range(-mapWidth / 2, mapWidth / 2), -mapHeight / 2 - 1, 0);
                break;
            case 3: // Top side
                spawnPosition = new Vector3(Random.Range(-mapWidth / 2, mapWidth / 2), mapHeight / 2 + 1, 0);
                break;
            default:
                spawnPosition = Vector3.zero;
                break;
        }

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
