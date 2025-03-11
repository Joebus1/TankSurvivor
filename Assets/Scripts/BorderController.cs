using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(EdgeCollider2D))]
public class BorderController : MonoBehaviour
{
    public float innerWidth = 16f;  // Inner playing field width
    public float innerHeight = 16f; // Inner playing field height
    public float borderThickness = 1f; // Thickness of the border (1 unit)

    private SpriteRenderer spriteRenderer;
    private EdgeCollider2D edgeCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        UpdateBorder();
    }

    void UpdateBorder()
    {
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

        // Optional: Adjust sprite tiling to show only the border (advanced)
        // For now, the tiling will cover the whole area, but the collider defines the collision
    }

    // Optional: For testing in the Inspector
    public void SetSize(float newInnerWidth, float newInnerHeight)
    {
        innerWidth = newInnerWidth;
        innerHeight = newInnerHeight;
        UpdateBorder();
    }
}