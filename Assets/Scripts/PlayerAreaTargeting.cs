using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAreaTargeting : MonoBehaviour
{
    public List<Vector3> tinyPositions = new List<Vector3>();
    public GameObject projectilePrefab; // Assign the projectile prefab in the inspector
    public float projectileSpeed = 5f; // Adjust the speed of the projectile

    private float fireInterval = 1.0f;
    private float nextFireTime = 0.0f;

    void OnTriggerStay2D(Collider2D collision)
    {
        // Clear the list before adding new positions
        tinyPositions.Clear();

        // Check for all objects in the trigger
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale / 2, 0f);
        foreach (Collider2D collider in colliders)
        {
            Debug.Log("THERE IS AN ITEM COLLIDING");
            if (collider.CompareTag("Tiny"))
            {
                TinyOwnerReference ownerRef = collider.GetComponent<TinyOwnerReference>();
                if (ownerRef != null && ownerRef.owner != null && ownerRef.owner != this.gameObject.transform.parent.gameObject)
                {
                    tinyPositions.Add(collider.transform.position);
                    Debug.Log(collider.transform.position);
                }
            }
        }
    }

    void Update()
    {
        if (Time.time >= nextFireTime && tinyPositions.Count > 0)
        {
            FireProjectiles();
            nextFireTime = Time.time + fireInterval;
        }
    }

    void FireProjectiles()
    {
        // Iterate over all children of the gameobject
        foreach (Transform child in transform)
        {
            // Check if the child has any children
            if (child.childCount > 0)
            {
                // Iterate over each child of the current child (grandchildren)
                foreach (Transform grandchild in child)
                {
                    // Select a random position from tinyPositions
                    Vector3 targetPosition = tinyPositions[Random.Range(0, tinyPositions.Count)];

                    // Instantiate the projectile at the grandchild's position
                    GameObject projectile = Instantiate(projectilePrefab, grandchild.position, Quaternion.identity);

                    // Calculate the direction to the target
                    Vector3 direction = (targetPosition - grandchild.position).normalized;

                    // Set the projectile's velocity
                    Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.velocity = direction * projectileSpeed;
                    }
                    else
                    {
                        Debug.LogWarning("Projectile prefab is missing a Rigidbody2D component.");
                    }
                }
            }
        }
    }
}
