using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(EdgeCollider2D))]
public class BorderController : MonoBehaviour
{
    public float width = 16f;  // Width of the border in Unity units
    public float height = 16f; // Height of the border in Unity units

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
        // Set the tiled sprite size
        spriteRenderer.size = new Vector2(width, height);

        // Define the collider points to form a rectangle
        Vector2[] points = new Vector2[5]
        {
            new Vector2(-width / 2, -height / 2), // Bottom-left
            new Vector2(width / 2, -height / 2),  // Bottom-right
            new Vector2(width / 2, height / 2),   // Top-right
            new Vector2(-width / 2, height / 2),  // Top-left
            new Vector2(-width / 2, -height / 2)  // Back to bottom-left to close the loop
        };
        edgeCollider.points = points;
    }

    // Optional: For testing in the Inspector
    public void SetSize(float newWidth, float newHeight)
    {
        width = newWidth;
        height = newHeight;
        UpdateBorder();
    }
}