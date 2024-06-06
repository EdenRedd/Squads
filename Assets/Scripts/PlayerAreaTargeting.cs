using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAreaTargeting : MonoBehaviour
{
    public List<Vector3> tinyPositions = new List<Vector3>();
    private float checkInterval = 1.0f;
    private float nextCheckTime = 0.0f;

    void OnTriggerStay(Collider other)
    {
        if (Time.time >= nextCheckTime)
        {
            // Clear the list before adding new positions
            tinyPositions.Clear();

            // Check for all objects in the trigger
            Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Tiny"))
                {
                    TinyOwnerReference ownerRef = collider.GetComponent<TinyOwnerReference>();
                    if (ownerRef != null && ownerRef.owner != null)
                    {
                        tinyPositions.Add(collider.transform.position);
                        Debug.Log(collider.transform.position);
                    }
                }
            }

            // Set the next check time
            nextCheckTime = Time.time + checkInterval;
        }
    }
}
