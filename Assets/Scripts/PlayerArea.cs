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

    
}
