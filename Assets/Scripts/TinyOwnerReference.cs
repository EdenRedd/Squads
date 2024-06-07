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

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;

        // Check if the collided object has the tag "Tiny"
        if (collidedObject.CompareTag("Area") && this.owner == null)
        {
            
                GameObject parent = collidedObject.transform.parent?.gameObject;
                if (parent != null && (parent.CompareTag("Player") || parent.CompareTag("Enemy")))
                {
                    this.owner = parent;
                }
            
            // Find the first childless GameObject
            GameObject firstChildlessGameObject = collision.gameObject.GetComponent<PlayerArea>().GetFirstChildlessGameObject();

            if (firstChildlessGameObject != null)
            {
                // Set the collided object as a child of the first childless GameObject
                this.transform.SetParent(firstChildlessGameObject.transform);

                // Reset the transform of the collided object
                this.transform.localPosition = new Vector3(0, 0, -24);
                this.transform.localRotation = Quaternion.identity;
                this.transform.localScale = Vector3.one;

                this.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                Debug.LogWarning("No childless GameObject available to parent the collided object.");
            }
        }
    }
}
