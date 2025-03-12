using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isGameOver = false;
    private PlayerHealth playerHealth; // Reference to player's health

    void Start()
    {
        // Find the player's health component at the start
        playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth not found in scene!");
        }
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            Debug.Log("Game Over! Press R to restart.");
            isGameOver = true;
            Time.timeScale = 0f;  // Pause the game
        }
    }

    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))  // Press "R" to restart
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;  // Resume game time
            isGameOver = false;

            // Reset the player's health
            if (playerHealth != null)
            {
                playerHealth.ResetHealth();
            }
        }
    }
}