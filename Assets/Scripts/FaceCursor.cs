using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCursor : MonoBehaviour
{
    void Update()
    {
        // Get the world position of the cursor
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = 0; // Ensure the z-coordinate is 0 for a 2D game

        // Calculate the direction from the weapon to the cursor
        Vector3 direction = cursorPosition - transform.position;

        // Calculate the angle between the weapon's forward direction and the direction to the cursor
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the weapon
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
