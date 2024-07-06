using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int minRoomSpawnCount, maxRoomSpawnCount, currentRoomCount = 0;
    public List<GameObject> spawnPositions = new List<GameObject>();
    public List<GameObject> rooms = new List<GameObject>();
    public GameObject spawnRoom;
    public int maxInstantiationRetry;

    public void Start()
    {
        List<GameObject> spawnRoomPositions = spawnRoom.GetComponent<RoomPositions>().spawnPositions;
        for (int f = 0; f < spawnRoomPositions.Count; f++)
        {
            spawnPositions.Add(spawnRoomPositions[f].gameObject);
        }

        while (spawnPositions.Count > 0)
        {
            int k = 0;
            while(k < maxInstantiationRetry)
            {
                GameObject room = null;
                bool roomSpawned = false;
                while (!roomSpawned)
                {
                    int indexOfRoomToInstantiate = Random.Range(0, rooms.Count);
                    if (spawnPositions[0].gameObject.name == "Top" && rooms[indexOfRoomToInstantiate].GetComponent<RoomPositions>().BottomTP != null)
                    {
                        Debug.Log("TOP");
                        room = GameObject.Instantiate(rooms[indexOfRoomToInstantiate], spawnPositions[0].transform.position, Quaternion.identity);
                        roomSpawned = true;
                        linkTeleporters(room.GetComponent<RoomPositions>().BottomTP, spawnPositions[0].transform.parent.parent.gameObject.GetComponent<RoomPositions>().TopTP);
                        if(room.GetComponent<RoomPositions>().spawnPositions != null)
                        {
                            List<string> gameObjectNames = new List<string>();

                            for (int n = 0; n < room.GetComponent<RoomPositions>().spawnPositions.Count; n++)
                            {
                                if (room.GetComponent<RoomPositions>().spawnPositions[n].name == "Bottom")
                                {
                                    room.GetComponent<RoomPositions>().spawnPositions.Remove(room.GetComponent<RoomPositions>().spawnPositions[n]);
                                }
                            }
                        }
                    }
                    else if (spawnPositions[0].gameObject.name == "Bottom" && rooms[indexOfRoomToInstantiate].GetComponent<RoomPositions>().TopTP != null)
                    {
                        Debug.Log("Bottom");
                        room = GameObject.Instantiate(rooms[indexOfRoomToInstantiate], spawnPositions[0].transform.position, Quaternion.identity);
                        roomSpawned = true;
                        linkTeleporters(room.GetComponent<RoomPositions>().TopTP, spawnPositions[0].transform.parent.parent.gameObject.GetComponent<RoomPositions>().BottomTP);
                        if (room.GetComponent<RoomPositions>().spawnPositions != null)
                        {
                            List<string> gameObjectNames = new List<string>();

                            for (int n = 0; n < room.GetComponent<RoomPositions>().spawnPositions.Count; n++)
                            {
                                if (room.GetComponent<RoomPositions>().spawnPositions[n].name == "Top")
                                {
                                    room.GetComponent<RoomPositions>().spawnPositions.Remove(room.GetComponent<RoomPositions>().spawnPositions[n]);
                                }
                            }
                        }
                    }
                    else if (spawnPositions[0].gameObject.name == "Left" && rooms[indexOfRoomToInstantiate].GetComponent<RoomPositions>().RightTP != null)
                    {
                        Debug.Log("left");
                        room = GameObject.Instantiate(rooms[indexOfRoomToInstantiate], spawnPositions[0].transform.position, Quaternion.identity);
                        roomSpawned = true;
                        linkTeleporters(room.GetComponent<RoomPositions>().RightTP, spawnPositions[0].transform.parent.parent.gameObject.GetComponent<RoomPositions>().LeftTP);
                        if (room.GetComponent<RoomPositions>().spawnPositions != null)
                        {
                            List<string> gameObjectNames = new List<string>();

                            for (int n = 0; n < room.GetComponent<RoomPositions>().spawnPositions.Count; n++)
                            {
                                if (room.GetComponent<RoomPositions>().spawnPositions[n].name == "Right")
                                {
                                    room.GetComponent<RoomPositions>().spawnPositions.Remove(room.GetComponent<RoomPositions>().spawnPositions[n]);
                                }
                            }
                        }
                    }
                    else if (spawnPositions[0].gameObject.name == "Right" && rooms[indexOfRoomToInstantiate].GetComponent<RoomPositions>().LeftTP != null)
                    {
                        Debug.Log("Left");
                        room = GameObject.Instantiate(rooms[indexOfRoomToInstantiate], spawnPositions[0].transform.position, Quaternion.identity);
                        roomSpawned = true;
                        linkTeleporters(room.GetComponent<RoomPositions>().LeftTP, spawnPositions[0].transform.parent.parent.gameObject.GetComponent<RoomPositions>().RightTP);
                        if (room.GetComponent<RoomPositions>().spawnPositions != null)
                        {
                            List<string> gameObjectNames = new List<string>();

                            for (int n = 0; n < room.GetComponent<RoomPositions>().spawnPositions.Count; n++)
                            {
                                if (room.GetComponent<RoomPositions>().spawnPositions[n].name == "Left")
                                {
                                    room.GetComponent<RoomPositions>().spawnPositions.Remove(room.GetComponent<RoomPositions>().spawnPositions[n]);
                                }
                            }
                        }
                    }
                }
                

                if (room.GetComponent<RoomPositions>() != null && room.GetComponent<RoomPositions>().isColliding)
                {
                    GameObject.Destroy(room);
                }
                else
                {
                    if(room.GetComponent<RoomPositions>().spawnPositions.Count <= 0)
                    {
                        currentRoomCount++;
                        break;
                    }
                    for(int j = 0; j < room.GetComponent<RoomPositions>().spawnPositions.Count; j++)
                    {
                        Debug.Log("CHANGED THE LIST" + j);
                        spawnPositions.Add(room.GetComponent<RoomPositions>().spawnPositions[j]);
                        currentRoomCount++;
                    }
                    break;
                }
                k++;
            }

            Debug.Log("removed " + spawnPositions[0]);
            spawnPositions.Remove(spawnPositions[0]);
        }

        MMCameraEvent.Trigger(MMCameraEventTypes.SetTargetCharacter, GameObject.FindGameObjectWithTag("Player").GetComponent<Character>());
    }

    public void linkTeleporters(Teleporter tp1, Teleporter tp2)
    {
        tp1.Destination = tp2;
        tp2.Destination = tp1;

        tp1.TargetRoom = tp2.transform.parent.parent.gameObject.GetComponent<Room>();
        tp2.TargetRoom = tp1.transform.parent.parent.gameObject.GetComponent<Room>();
    }
}
