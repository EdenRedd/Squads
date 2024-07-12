using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class RoomInformation : MonoBehaviour
{
    public Tilemap objstaclesTilemap;
    public Tilemap groundTilemap;
    public EnemiesInRoomTracker enemiesInRoomTracker;

    public bool IsCleared = false;
}
