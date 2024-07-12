using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
 * - This script should be updated through events what room we are currently in
 * - This script should hold combinations of waves to spawn 
 * - This script should have functions the KillManager can call upon to decide what to do next such as shouldSpawn wave? or areWe at max waves
 * - 
 */
public class WaveManager : MonoBehaviour
{
    private int currentRoomGameObjectId;
    public int enemyMinAmountToSpawn = 1;

    public int enemyMaxAmountToSpawn = 4;

    public GameObject enemyPrefab;
    public Tilemap groundTilemap;
    public Tilemap obstaclesTilemap;
    public EnemiesInRoomTracker enemiesInRoomTracker;

    public int maxWaves = 2;
    public int currentWave = 1;

    public static WaveManager instance = null;

    void Awake()
    {
        Debug.Log("We woke up the Wave manager");
        // Check if instance already exists
        if (instance == null)
            // If not, set instance to this
            instance = this;
        // If instance already exists and it's not this, destroy this.
        // This enforces the singleton pattern, meaning there can only ever be one instance.
        else if (instance != this)
            Destroy(gameObject);

        // Sets this to not be destroyed when reloading the scene
        DontDestroyOnLoad(gameObject);
    }

    public void IncreaseWave()
    {
        currentWave++;
    }

    public bool WaveAtMaxCheck()
    {
        if(currentWave == maxWaves)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //funtion to set the next wave to spawn

    public void CallOnWaveFinish()
    {
        if(WaveAtMaxCheck())
        {
            //unlock teleporters? and spawn in chest
            enemiesInRoomTracker.SpawnChest();
            enemiesInRoomTracker.UnlockTeleporters();
        }
        else
        {
            IncreaseWave();
            ManagerSpawnInWave();
            KillsManager.instance.RefreshRemainingDeaths();
        }
    }

    public void ManagerSpawnInWave()
    {
        PlaceEnemies(true, this.groundTilemap, this.obstaclesTilemap, this.enemiesInRoomTracker);
    }

    public void PlaceEnemies(bool isSpawnRoom, Tilemap groundTileMap, Tilemap obstaclesTilemap, EnemiesInRoomTracker enemiesInRoomTracker)
    {
        if (isSpawnRoom)
        {
            int enemyAmountToSpawn = UnityEngine.Random.Range(enemyMinAmountToSpawn, enemyMaxAmountToSpawn);
            for (int i = 0; i < enemyAmountToSpawn; i++)
            {
                Vector3 spawnPosition = GetRandomTilePosition(groundTileMap, obstaclesTilemap);

                GameObject enemy = GameObject.Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                enemiesInRoomTracker.enemiesInRoom.Add(enemy.GetInstanceID());


                KillsManager.Instance.TargetsList.Add(enemy.GetComponent<Health>());
                this.groundTilemap = groundTileMap;
                this.obstaclesTilemap = obstaclesTilemap;
                this.enemiesInRoomTracker = enemiesInRoomTracker;

            }
            KillsManager.Instance.RefreshRemainingDeaths();
        }
    }

    public void ResetWaves()
    {
        this.currentWave = 1;
    }

    public Vector3 GetRandomTilePosition(Tilemap groundTileMap, Tilemap obstaclesTilemap)
    {
        // Get the bounds of the primary Tilemap
        BoundsInt bounds = groundTileMap.cellBounds;

        // Create a list to store all possible tile positions
        List<Vector3Int> validTilePositions = new List<Vector3Int>();

        // Iterate through all tiles in the bounds of the primary Tilemap
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int localPlace = new Vector3Int(x, y, (int)groundTileMap.transform.position.z);

                // Check if there's a tile in the primary Tilemap and no tile in the exclusion Tilemap
                if (groundTileMap.HasTile(localPlace) && !obstaclesTilemap.HasTile(localPlace))
                {
                    validTilePositions.Add(localPlace);
                }
            }
        }

        // Select a random tile position from the list
        if (validTilePositions.Count > 0)
        {
            Vector3Int randomTilePosition = validTilePositions[Random.Range(0, validTilePositions.Count)];
            Vector3 worldPosition = groundTileMap.CellToWorld(randomTilePosition);
            return worldPosition;
        }

        // If no valid tiles found, return zero vector (or handle accordingly)
        return Vector3.zero;
    }
}
