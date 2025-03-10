using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    public float defaultZoom = 3f;  // Set your starting zoom (closer view)
    public float zoomSpeed = 2f;    // Speed of zooming
    public float minZoom = 2f;      // Closest zoom-in
    public float maxZoom = 8f;      // Furthest zoom-out

    void Start()
    {
        if (cam == null)
            cam = Camera.main;

        cam.orthographicSize = defaultZoom;  // Set the starting zoom
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            cam.orthographicSize -= scroll * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }
    }
}
