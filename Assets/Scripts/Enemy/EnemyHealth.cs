using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // The maximum health of the enemy
    private int currentHealth;

    void Start()
    {
        // Initialize the current health to the maximum health at the start
        currentHealth = maxHealth;
    }

    // Function to subtract health
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Enemy took damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Function to add health
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("Enemy healed. Current health: " + currentHealth);
    }

    // Function to handle the enemy's death
    void Die()
    {
        Debug.Log("Enemy died.");
        // You can add any additional logic for when the enemy dies here (e.g., play animation, destroy object)
        Destroy(gameObject);
    }
}
