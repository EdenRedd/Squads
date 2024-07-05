using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPositions : MonoBehaviour
{
    public List<Vector3> spawnPositions = new List<Vector3>();
    public bool isColliding = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
    }
}
