using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TeleporterUse : MonoBehaviour
{
    public void UpdateWaveManager()
    {
        Teleporter teleporter = this.GetComponent<Teleporter>();
        
        RoomInformation destinationRoomInfo = teleporter.TargetRoom.gameObject.GetComponent<RoomInformation>();

        if (!destinationRoomInfo.IsCleared)
        {

            WaveManager.instance.PlaceEnemies(true, destinationRoomInfo.groundTilemap, destinationRoomInfo.objstaclesTilemap, destinationRoomInfo.enemiesInRoomTracker);
            WaveManager.instance.ResetWaves();

            RoomInformation currentRoomInfo = teleporter.CurrentRoom.GetComponent<RoomInformation>();
            currentRoomInfo.IsCleared = true;
        }
    }
}
