using UnityEngine;

public class CameraController : MonoBehaviour
{
    private MapSettings mapSettings;
    private Camera mainCamera;
    public float zoomSpeed = 2f; // Speed of zooming with the mouse wheel
    public float minZoom = 5f; // Minimum orthographic size (zoomed in)
    public float maxZoom = 15f; // Maximum orthographic size (zoomed out)

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found!");
            return;
        }

        mapSettings = MapSettings.Instance;
        if (mapSettings == null)
        {
            Debug.LogError("MapSettings not found in scene!");
            return;
        }

        AdjustCamera();
    }

    void Update()
    {
        // Handle mouse wheel zoom
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            // Adjust the orthographic size based on scroll input
            float newSize = mainCamera.orthographicSize - scrollInput * zoomSpeed;
            // Clamp the zoom between minZoom and maxZoom
            mainCamera.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        }
    }

    void AdjustCamera()
    {
        // Get map size from MapSettings
        float mapWidth = mapSettings.GetMapWidth();
        float mapHeight = mapSettings.GetMapHeight();
        float borderThickness = mapSettings.GetBorderThickness();

        // Calculate the total size including borders
        float totalWidth = mapWidth + 2 * borderThickness;
        float totalHeight = mapHeight + 2 * borderThickness;

        // Set the default orthographic size to fit the map (used as initial zoom)
        float defaultSize = Mathf.Max(totalWidth, totalHeight) / 2f + 1f; // Add padding
        mainCamera.orthographicSize = defaultSize;

        // Update maxZoom to be slightly larger than the default size
        maxZoom = defaultSize + 5f;
        // Update minZoom to be smaller but not too close
        minZoom = Mathf.Max(5f, defaultSize / 2f);
    }
}