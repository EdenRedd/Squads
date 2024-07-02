using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTeleporter : MonoBehaviour, MMEventListener<MMGameEvent>
{

    public List<int> gameObjects = new List<int>();

    // Method to remove a specific GameObject from the list if it matches by instance ID
    public void RemoveMatchingGameObject(int targetID)
    {
        for (int i = gameObjects.Count - 1; i >= 0; i--)
        {
            if (gameObjects[i] == targetID)
            {
                gameObjects.RemoveAt(i);
            }
        }
    }



    void MMEventListener<MMGameEvent>.OnMMEvent(MMGameEvent eventType)
    {
        if(eventType.EventName == "CharacterDeath")
        {
            RemoveMatchingGameObject(eventType.IntParameter);
            if(gameObjects.Count <= 0)
            {
                this.gameObject.GetComponent<Teleporter>().Activable = true;
            }
        }

        if(eventType.EventName == "SpawnedEnemy")
        {
            gameObjects.Add(eventType.IntParameter);
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
