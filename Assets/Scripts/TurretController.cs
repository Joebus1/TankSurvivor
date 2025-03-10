using UnityEngine;

public class TurretController : MonoBehaviour
{
    void Update()
    {
        // Get mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate angle between turret and mouse
        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Adjust the offset (if turret is rotated 90 degrees off)
        angle -= -90f;  // Try changing this if needed

        // Rotate turret
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
