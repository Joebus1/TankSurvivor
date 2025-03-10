using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public float initialSpawnRate = 5f;  // Start with 5 seconds between spawns
    public float spawnRateIncrease = 0.5f;  // How much faster spawning gets over time
    public float increaseInterval = 5f;  // Every 5 seconds, spawn rate increases

    private float currentSpawnRate;

    void Start()
    {
        currentSpawnRate = initialSpawnRate;
        StartCoroutine(SpawnEnemies());
        StartCoroutine(IncreaseSpawnRate());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentSpawnRate);
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (player == null) return;

        float spawnDistance = 10f;
        Vector2 spawnDirection = Random.insideUnitCircle.normalized;
        Vector2 spawnPosition = (Vector2)player.position + (spawnDirection * spawnDistance);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    IEnumerator IncreaseSpawnRate()
    {
        while (true)
        {
            yield return new WaitForSeconds(increaseInterval);
            currentSpawnRate = Mathf.Max(0.5f, currentSpawnRate - spawnRateIncrease); // Minimum limit to prevent instant spawning
            Debug.Log("New spawn rate: " + currentSpawnRate);
        }
    }
}
