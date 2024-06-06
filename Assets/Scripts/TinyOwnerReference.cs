using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyOwnerReference : MonoBehaviour
{
    public GameObject owner;
    void Start()
    {
        // Attempt to assign the owner on start
        AssignOwner();
    }

    void AssignOwner()
    {
        // Find all colliders in a specific range and check for potential owners
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.0f); // Adjust range as necessary
        foreach (Collider2D collider in colliders)
        {
            if (owner == null)
            {
                GameObject parent = collider.transform.parent?.gameObject;
                if (parent != null && (parent.CompareTag("Player") || parent.CompareTag("Enemy")))
                {
                    owner = parent;
                    break;
                }
            }
        }
    }
}
