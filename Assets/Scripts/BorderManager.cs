using UnityEngine;

public class BorderManager : MonoBehaviour
{
    public GameObject borderPrefab; // Assign your border prefab in the inspector
    public int width = 16; // Change to match your play area
    public int height = 16;

    private void Start()
    {
        GenerateBorders();
    }

    void GenerateBorders()
    {
        // Left & Right Borders
        for (int y = -height / 2; y <= height / 2; y++)
        {
            Instantiate(borderPrefab, new Vector3(-width / 2, y, 0), Quaternion.identity, transform);
            Instantiate(borderPrefab, new Vector3(width / 2, y, 0), Quaternion.identity, transform);
        }

        // Top & Bottom Borders
        for (int x = -width / 2; x <= width / 2; x++)
        {
            Instantiate(borderPrefab, new Vector3(x, -height / 2, 0), Quaternion.identity, transform);
            Instantiate(borderPrefab, new Vector3(x, height / 2, 0), Quaternion.identity, transform);
        }
    }
}
