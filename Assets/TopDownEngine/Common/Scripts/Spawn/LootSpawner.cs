using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    public GameObject lootToSpawn;
    public void spawnChest()
    {
        GameObject.Instantiate(lootToSpawn, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
    }
}
