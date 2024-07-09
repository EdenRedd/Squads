using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    public GameObject lootToSpawn;
    public Transform positionToSpawnAt;
    public void spawnChest()
    {
        GameObject.Instantiate(lootToSpawn, positionToSpawnAt.position, Quaternion.identity);
    }
}
