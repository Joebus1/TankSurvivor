using UnityEngine;

public class MapSettings : MonoBehaviour
{
    // Singleton instance to access settings globally
    public static MapSettings Instance { get; private set; }

    [Header("Map Size Settings")]
    public float mapWidth = 16f;  // Width of the map (X-axis)
    public float mapHeight = 16f; // Height of the map (Y-axis)
    public float borderThickness = 1f; // Thickness of the border

    void Awake()
    {
        // Set up the singleton instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes (optional)
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    // Optional: Getter methods if you prefer accessing values this way
    public float GetMapWidth() => mapWidth;
    public float GetMapHeight() => mapHeight;
    public float GetBorderThickness() => borderThickness;
}
