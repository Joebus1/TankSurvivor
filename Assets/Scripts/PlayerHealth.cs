using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // Maximum health points for the player
    private int currentHealth; // Current health points
    public TMP_Text healthText; // Reference to TextMeshPro Text (assign in Inspector)
    public HealthBarController healthBarController; // Reference to HealthBarController

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI(); // Update the UI to show initial health
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Player health: {currentHealth}");
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player destroyed! Game Over!");
        gameObject.SetActive(false);
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager != null)
        {
            gameManager.GameOver();
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        gameObject.SetActive(true);
    }

    void UpdateHealthUI()
    {
        if (healthText != null) // Check if a UI Text is assigned
        {
            healthText.text = $"Health: {currentHealth}/{maxHealth}";
            Debug.Log($"Updating health UI to: {healthText.text}"); // Debug log
        }
        if (healthBarController != null) // Check if HealthBarController is assigned
        {
            healthBarController.UpdateHealthBar(currentHealth, maxHealth);
        }
    }
}