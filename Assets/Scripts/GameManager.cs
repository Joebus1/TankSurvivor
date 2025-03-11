using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isGameOver = false;

    public void GameOver()
    {
        if (!isGameOver)
        {
            Debug.Log("Game Over! Press R to restart.");
            isGameOver = true;
            Time.timeScale = 0f;  // Pauses the game
        }
    }

    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))  // Press "R" to restart
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;  // Resume game time
            isGameOver = false;
        }
    }
}