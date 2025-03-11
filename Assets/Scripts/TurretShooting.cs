using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    public GameObject bulletPrefab;  // Assign in Inspector
    public Transform firePoint;  // Empty object for spawn position
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;  // Fire every 0.5s
    private float nextFireTime = 0f;

    void Start()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("Bullet Prefab or Fire Point not assigned on " + gameObject.name);
        }
    }

    void Update()
    {
        Debug.Log("Fire Rate: " + fireRate); // This will show the current fire rate in the console
       
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)  // Left click to shoot
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
{
    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    bullet.layer = LayerMask.NameToLayer("Bullet"); // Set bullet layer
    Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
    if (bulletRb != null)
    {
        bulletRb.bodyType = RigidbodyType2D.Dynamic;
        bulletRb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        bulletRb.linearVelocity = firePoint.up * bulletSpeed;
    }
    bullet.SetActive(true); // Ensure the bullet is active
}
    void OnDrawGizmos()
{
    if (firePoint != null)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(firePoint.position, 0.1f);
    }
}
}