using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if (Time.timeScale == 0f && Input.GetKeyDown(KeyCode.R))  // Press "R" to restart
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;  // Resume game time
        }
    }
}
