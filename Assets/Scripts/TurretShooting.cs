using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    public Transform firePoint;
    public ParticleSystem muzzleFlash; // Reference to the muzzle flash particle system

    private float nextFireTime;

    void Update()
    {
        // Check for Space key or left mouse button
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            if (muzzleFlash != null)
            {
                muzzleFlash.Clear();
                muzzleFlash.Play();
                Debug.Log("Muzzle flash played!");
            }
            else
            {
                Debug.LogWarning("MuzzleFlash is not assigned or not found!");
            }
        }
        else
        {
            Debug.LogWarning("bulletPrefab or firePoint is not assigned!");
        }
    }
}