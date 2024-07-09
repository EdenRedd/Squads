using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * What this does
 * This Manager uses the rooms given to spawn a randomly generated dungeon with a random 
 * room connected to an available portal to the spawn room and then to that room and so on.
 * 
 * What it should do in the future
 * Needs to stop after a certain amount of iterations
 * Needs to be able to recognize overlaps in rooms
 * Needs to have a minimum amount of rooms
 * Needs to not crash the unity editor when run
 * Needs to also set an end to the dungeon
 * Maybe needs a start room to spawn as well
 * 
 * What it needs in the scene to work:
 * A spawn room with spawn positions GO with children (TOP, BOTTOM, LEFT, RIGHT)
 * Rooms that also have spawn positions GO with children (TOP, BOTTOM, LEFT, RIGHT)
 * A RoomPositions script on every room that specifies what teleporters it has active and the positions where the teleporters are
 */
public class RoomManager : MonoBehaviour
{
    //Setting these is not used right now and it is not tracked accuratley
    public int minRoomSpawnCount, maxRoomSpawnCount, currentRoomCount = 0;
    public List<GameObject> roomSpawnPositions = new List<GameObject>();
    public List<GameObject> roomPrefabs = new List<GameObject>();
    public GameObject spawnRoomInScene;
    public int maxInstantiationRetry; //not working as of now, so will not stop trying to spawn after this amount

    public void Start()
    {
        List<GameObject> spawnRoomPositions = spawnRoomInScene.GetComponent<RoomPositions>().spawnPositions;
        for (int f = 0; f < spawnRoomPositions.Count; f++)
        {
            roomSpawnPositions.Add(spawnRoomPositions[f].gameObject);
        }

        while (roomSpawnPositions.Count > 0)
        {
            int k = 0;
            while(k < maxInstantiationRetry)    //Does not work k never increases or reached maxInstantiationRetry because we do no detect collision between the rooms correctly
            {
                GameObject roomToSpawn = null;
                bool roomSpawned = false;

                while (!roomSpawned)
                {
                    int indexOfRoomToInstantiate = Random.Range(0, roomPrefabs.Count);

                    if (roomSpawnPositions[0].gameObject.name == "Top" && roomPrefabs[indexOfRoomToInstantiate].GetComponent<RoomPositions>().BottomTP != null)
                    {
                        roomToSpawn = GameObject.Instantiate(roomPrefabs[indexOfRoomToInstantiate], roomSpawnPositions[0].transform.position, Quaternion.identity);
                        roomSpawned = true;
                        linkTeleporters(roomToSpawn.GetComponent<RoomPositions>().BottomTP, roomSpawnPositions[0].transform.parent.parent.gameObject.GetComponent<RoomPositions>().TopTP);
                        
                        //Look into the room we already spawned and remove the positions to spawn a new room if we already spawned a room there.
                        if(roomToSpawn.GetComponent<RoomPositions>().spawnPositions != null)
                        {
                            List<string> gameObjectNames = new List<string>();

                            for (int n = 0; n < roomToSpawn.GetComponent<RoomPositions>().spawnPositions.Count; n++)
                            {
                                if (roomToSpawn.GetComponent<RoomPositions>().spawnPositions[n].name == "Bottom")
                                {
                                    roomToSpawn.GetComponent<RoomPositions>().spawnPositions.Remove(roomToSpawn.GetComponent<RoomPositions>().spawnPositions[n]);
                                }
                            }
                        }
                    }
                    else if (roomSpawnPositions[0].gameObject.name == "Bottom" && roomPrefabs[indexOfRoomToInstantiate].GetComponent<RoomPositions>().TopTP != null)
                    {
                        roomToSpawn = GameObject.Instantiate(roomPrefabs[indexOfRoomToInstantiate], roomSpawnPositions[0].transform.position, Quaternion.identity);
                        roomSpawned = true;
                        linkTeleporters(roomToSpawn.GetComponent<RoomPositions>().TopTP, roomSpawnPositions[0].transform.parent.parent.gameObject.GetComponent<RoomPositions>().BottomTP);

                        //Look into the room we already spawned and remove the positions to spawn a new room if we already spawned a room there.
                        if (roomToSpawn.GetComponent<RoomPositions>().spawnPositions != null)
                        {
                            List<string> gameObjectNames = new List<string>();

                            for (int n = 0; n < roomToSpawn.GetComponent<RoomPositions>().spawnPositions.Count; n++)
                            {
                                if (roomToSpawn.GetComponent<RoomPositions>().spawnPositions[n].name == "Top")
                                {
                                    roomToSpawn.GetComponent<RoomPositions>().spawnPositions.Remove(roomToSpawn.GetComponent<RoomPositions>().spawnPositions[n]);
                                }
                            }
                        }
                    }
                    else if (roomSpawnPositions[0].gameObject.name == "Left" && roomPrefabs[indexOfRoomToInstantiate].GetComponent<RoomPositions>().RightTP != null)
                    {
                        roomToSpawn = GameObject.Instantiate(roomPrefabs[indexOfRoomToInstantiate], roomSpawnPositions[0].transform.position, Quaternion.identity);
                        roomSpawned = true;
                        linkTeleporters(roomToSpawn.GetComponent<RoomPositions>().RightTP, roomSpawnPositions[0].transform.parent.parent.gameObject.GetComponent<RoomPositions>().LeftTP);

                        //Look into the room we already spawned and remove the positions to spawn a new room if we already spawned a room there.
                        if (roomToSpawn.GetComponent<RoomPositions>().spawnPositions != null)
                        {
                            List<string> gameObjectNames = new List<string>();

                            for (int n = 0; n < roomToSpawn.GetComponent<RoomPositions>().spawnPositions.Count; n++)
                            {
                                if (roomToSpawn.GetComponent<RoomPositions>().spawnPositions[n].name == "Right")
                                {
                                    roomToSpawn.GetComponent<RoomPositions>().spawnPositions.Remove(roomToSpawn.GetComponent<RoomPositions>().spawnPositions[n]);
                                }
                            }
                        }
                    }
                    else if (roomSpawnPositions[0].gameObject.name == "Right" && roomPrefabs[indexOfRoomToInstantiate].GetComponent<RoomPositions>().LeftTP != null)
                    {
                        roomToSpawn = GameObject.Instantiate(roomPrefabs[indexOfRoomToInstantiate], roomSpawnPositions[0].transform.position, Quaternion.identity);
                        roomSpawned = true;
                        linkTeleporters(roomToSpawn.GetComponent<RoomPositions>().LeftTP, roomSpawnPositions[0].transform.parent.parent.gameObject.GetComponent<RoomPositions>().RightTP);

                        //Look into the room we already spawned and remove the positions to spawn a new room if we already spawned a room there.
                        if (roomToSpawn.GetComponent<RoomPositions>().spawnPositions != null)
                        {
                            List<string> gameObjectNames = new List<string>();

                            for (int n = 0; n < roomToSpawn.GetComponent<RoomPositions>().spawnPositions.Count; n++)
                            {
                                if (roomToSpawn.GetComponent<RoomPositions>().spawnPositions[n].name == "Left")
                                {
                                    roomToSpawn.GetComponent<RoomPositions>().spawnPositions.Remove(roomToSpawn.GetComponent<RoomPositions>().spawnPositions[n]);
                                }
                            }
                        }
                    }
                }
                
                //This attempts to delete the gameobject if our room we spawned in is colliding with another room. Does not work.
                if (roomToSpawn.GetComponent<RoomPositions>() != null && roomToSpawn.GetComponent<RoomPositions>().isColliding)
                {
                    GameObject.Destroy(roomToSpawn);
                }
                else //Goes into this else every time because the first if is never hit
                {
                    if(roomToSpawn.GetComponent<RoomPositions>().spawnPositions.Count <= 0) //if there are no more rooms to add we break out of the iteration while loop
                    {
                        currentRoomCount++;
                        break;
                    }
                    //We add all the positions the room we spawned in needs.
                    for(int j = 0; j < roomToSpawn.GetComponent<RoomPositions>().spawnPositions.Count; j++)
                    {
                        roomSpawnPositions.Add(roomToSpawn.GetComponent<RoomPositions>().spawnPositions[j]);
                        currentRoomCount++;
                    }
                    break;
                }
                k++;
            }
            roomSpawnPositions.Remove(roomSpawnPositions[0]);
        }

        //Trigger the event to set the spawned in rooms main character (should probablu optimize the find player with tag)
        MMCameraEvent.Trigger(MMCameraEventTypes.SetTargetCharacter, GameObject.FindGameObjectWithTag("Player").GetComponent<Character>());
    }

    //This function links two teleporters together by providing the necessesary info on them. assumes the TP is th child of a child within the room gameobject
    public void linkTeleporters(Teleporter tp1, Teleporter tp2)
    {
        tp1.Destination = tp2;
        tp2.Destination = tp1;

        tp1.TargetRoom = tp2.transform.parent.parent.gameObject.GetComponent<Room>();
        tp2.TargetRoom = tp1.transform.parent.parent.gameObject.GetComponent<Room>();
    }
}
