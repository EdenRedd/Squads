using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    public GameObject projectilePrefab; // The projectile prefab to instantiate
    public float projectileForce = 10f; // The force applied to the projectile

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate the projectile at the weapon's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

        // Get the Rigidbody2D component of the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Apply force to the projectile in the direction of the weapon's forward vector
            rb.AddForce(transform.right * projectileForce, ForceMode2D.Impulse);
        }
    }
}
