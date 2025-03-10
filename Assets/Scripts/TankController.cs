using UnityEngine;

public class TankController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;

    void Update()
    {
        float moveInput = Input.GetAxis("Vertical"); // W/S or Up/Down arrows
        float rotateInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows

        // Move forward or backward
        transform.Translate(Vector3.up * -moveInput * moveSpeed * Time.deltaTime);

        // Rotate the tank
        transform.Rotate(Vector3.forward, -rotateInput * rotationSpeed * Time.deltaTime);
    }
}
