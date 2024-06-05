using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArea : MonoBehaviour
{
    public List<GameObject> childGameObjects = new List<GameObject>();

    void Start()
    {
        // Populate the list with all child GameObjects
        foreach (Transform child in transform)
        {
            childGameObjects.Add(child.gameObject);
        }
    }

    // Function to get the first GameObject in the list that has no children
    public GameObject GetFirstChildlessGameObject()
    {
        foreach (GameObject obj in childGameObjects)
        {
            if (obj.transform.childCount == 0)
            {
                return obj;
            }
        }
        return null; // Return null if no childless GameObject is found
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;

        // Check if the collided object has the tag "Tiny"
        if (collidedObject.CompareTag("Tiny"))
        {
            // Find the first childless GameObject
            GameObject firstChildlessGameObject = GetFirstChildlessGameObject();

            if (firstChildlessGameObject != null)
            {
                // Set the collided object as a child of the first childless GameObject
                collidedObject.transform.SetParent(firstChildlessGameObject.transform);

                // Reset the transform of the collided object
                collidedObject.transform.localPosition = new Vector3(0, 0, -24);
                collidedObject.transform.localRotation = Quaternion.identity;
                collidedObject.transform.localScale = Vector3.one;

                collidedObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                Debug.LogWarning("No childless GameObject available to parent the collided object.");
            }
        }
    }
}
