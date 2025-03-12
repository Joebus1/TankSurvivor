using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider healthSlider; // Reference to the Slider
    private Image fillImage; // Reference to the fill image

    void Start()
    {
        // Get the fill image from the Slider's fill area
        fillImage = healthSlider.fillRect.GetComponent<Image>();
        if (fillImage == null)
        {
            Debug.LogError("Fill Image not found on HealthBar!");
        }
    }

    // Update the health bar value and color
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthSlider.value = currentHealth; // Set the slider value

        // Change color based on health percentage
        float healthPercentage = currentHealth / maxHealth;
        if (healthPercentage == 1f)
        {
            fillImage.color = Color.green; // Full health
        }
        else if (healthPercentage >= 0.5f)
        {
            fillImage.color = Color.yellow; // Half health
        }
        else
        {
            fillImage.color = Color.red; // Low health
        }
    }
}