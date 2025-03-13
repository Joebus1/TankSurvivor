using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapScaler : MonoBehaviour
{
    public TileBase grassTile; // The grass tile to fill the map with
    private Tilemap tilemap;
    private MapSettings mapSettings;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        if (tilemap == null)
        {
            Debug.LogError("TilemapScaler requires a Tilemap component!");
            return;
        }

        mapSettings = MapSettings.Instance;
        if (mapSettings == null)
        {
            Debug.LogError("MapSettings not found in scene!");
            return;
        }

        ResizeTilemap();
    }

    void ResizeTilemap()
    {
        // Get map size from MapSettings
        int mapWidth = (int)mapSettings.GetMapWidth();
        int mapHeight = (int)mapSettings.GetMapHeight();

        // Clear the existing tilemap
        tilemap.ClearAllTiles();

        // Calculate the bounds for the tilemap (centered at origin)
        int minX = -mapWidth / 2;
        int maxX = mapWidth / 2;
        int minY = -mapHeight / 2;
        int maxY = mapHeight / 2;

        // Fill the tilemap with grass tiles
        for (int x = minX; x < maxX; x++)
        {
            for (int y = minY; y < maxY; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), grassTile);
            }
        }

        Debug.Log($"Tilemap resized to {mapWidth}x{mapHeight}");
    }
}