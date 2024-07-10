using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesInRoomTracker : MonoBehaviour, MMEventListener<MMGameEvent>
{
    public List<int> enemiesInRoom = new List<int>();
    public LootSpawner spawner;
    public List<LockTeleporter> lockTeleporterList;

    public void OnMMEvent(MMGameEvent eventType)
    {
        if (eventType.EventName == "CharacterDeath")
        {
            RemoveMatchingGameObject(eventType.IntParameter);
            if (enemiesInRoom.Count <= 0)
            {
                spawner.spawnChest();
                for(int i = 0; i < lockTeleporterList.Count; i++)
                {
                    lockTeleporterList[i].enableTeleporter();
                }
            }
        }
    }

    // Method to remove a specific GameObject from the list if it matches by instance ID
    public void RemoveMatchingGameObject(int targetID)
    {
        for (int i = enemiesInRoom.Count - 1; i >= 0; i--)
        {
            if (enemiesInRoom[i] == targetID)
            {
                enemiesInRoom.RemoveAt(i);
            }
        }
    }

    void OnEnable()
    {
        this.MMEventStartListening<MMGameEvent>();
    }
    void OnDisable()
    {
        this.MMEventStopListening<MMGameEvent>();
    }
}