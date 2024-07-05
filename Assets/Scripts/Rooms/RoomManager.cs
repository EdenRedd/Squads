using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int minRoomSpawnCount, maxRoomSpawnCount, currentRoomCount = 0;
    public List<Vector3> spawnPositions = new List<Vector3>();
    public List<GameObject> rooms = new List<GameObject>();
    public GameObject spawnRoom;
    public int maxInstantiationRetry;

    //Spawn prefabs then after the spawn collect the spawnPositions
    //Add the spawn positions to the spawn positions
    //Remove the spawn position that was just used
    //iterate over spawnPositions until it is empty or the spawn room count has been reached

    public void Start()
    {
        List<Vector3> spawnRoomPositions = spawnRoom.GetComponent<RoomPositions>().spawnPositions;
        for (int i = 0; i < spawnRoomPositions.Count; i++)
        {
            spawnPositions.Add(spawnRoomPositions[i]);
        }

        for (int i = 0; i < spawnPositions.Count; i++)
        {
            int k = 0;
            while(k < maxInstantiationRetry)
            {
                GameObject room = GameObject.Instantiate(rooms[Random.Range(1, rooms.Count + 1)], spawnPositions[i], Quaternion.identity);

                if (room.GetComponent<RoomPositions>() != null && room.GetComponent<RoomPositions>().isColliding)
                {
                    GameObject.Destroy(room);
                }
                else
                {
                    for(int j = 0; j < room.GetComponent<RoomPositions>().spawnPositions.Count; j++)
                    {
                        spawnPositions.Add(room.GetComponent<RoomPositions>().spawnPositions[j]);
                        currentRoomCount++;
                    }
                }
            }


        }
    }
}
