using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(EdgeCollider2D))]
public class BorderController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private EdgeCollider2D edgeCollider;
    private MapSettings mapSettings; // Reference to MapSettings

    void Start()
    {
        // Find the MapSettings instance
        mapSettings = MapSettings.Instance;
        if (mapSettings == null)
        {
            Debug.LogError("MapSettings not found in scene!");
            return;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        UpdateBorder();
    }

    void UpdateBorder()
    {
        // Get map size and border thickness from MapSettings
        float innerWidth = mapSettings.GetMapWidth();
        float innerHeight = mapSettings.GetMapHeight();
        float borderThickness = mapSettings.GetBorderThickness();

        // Total size includes border on both sides
        float totalWidth = innerWidth + 2 * borderThickness;
        float totalHeight = innerHeight + 2 * borderThickness;

        // Set the SpriteRenderer size to the total area (for visibility)
        spriteRenderer.size = new Vector2(totalWidth, totalHeight);

        // Define collider points for the inner and outer edges
        Vector2[] points = new Vector2[5]
        {
            new Vector2(-totalWidth / 2, -totalHeight / 2), // Bottom-left (outer)
            new Vector2(totalWidth / 2, -totalHeight / 2),  // Bottom-right (outer)
            new Vector2(totalWidth / 2, totalHeight / 2),   // Top-right (outer)
            new Vector2(-totalWidth / 2, totalHeight / 2),  // Top-left (outer)
            new Vector2(-totalWidth / 2, -totalHeight / 2)  // Back to bottom-left
        };
        edgeCollider.points = points;
    }

    // Optional: For testing in the Inspector
    public void SetSize(float newInnerWidth, float newInnerHeight)
    {
        // Update MapSettings instead of local variables
        if (mapSettings != null)
        {
            mapSettings.mapWidth = newInnerWidth;
            mapSettings.mapHeight = newInnerHeight;
            UpdateBorder();
        }
    }
}