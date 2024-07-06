using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPositions : MonoBehaviour
{
    public List<GameObject> spawnPositions = new List<GameObject>();
    //Just list the TPS
    public Teleporter TopTP;
    public Teleporter BottomTP;
    public Teleporter LeftTP;
    public Teleporter RightTP;
    public bool isColliding = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Room")
        {
            isColliding = true;
        }
    }
}
