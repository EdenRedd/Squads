using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodge : MonoBehaviour
{
    public float shiftDistance = 2f; // Distance to shift the player forward

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the player is moving
        if (rb.velocity.magnitude > 0.1f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShiftForward();
            }
        }
    }

    void ShiftForward()
    {
        // Calculate the new position by moving forward in the direction of the velocity
        Vector2 newPosition = (Vector2)transform.position + rb.velocity.normalized * shiftDistance;
        transform.position = newPosition;
    }
}
