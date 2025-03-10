using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapFiller : MonoBehaviour
{
    public Tilemap tilemap;    // Assign your grass tilemap here
    public TileBase grassTile; // Assign your grass tile here
    public int width = 20;     // Match your border width
    public int height = 15;    // Match your border height

    void Start()
    {
        FillTilemap();
    }

    void FillTilemap()
    {
        for (int x = -width / 2; x < width / 2; x++)
        {
            for (int y = -height / 2; y < height / 2; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), grassTile);
            }
        }
    }
}