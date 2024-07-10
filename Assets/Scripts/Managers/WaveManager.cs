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

    public Tilemap GroundTilemap;

    public Tilemap ObstaclesTilemap;

    protected virtual void PlaceEnemies()
    {
        int enemyAmountToSpawn = UnityEngine.Random.Range(enemyMinAmountToSpawn, enemyMaxAmountToSpawn);
        for (int i = 0; i < enemyAmountToSpawn; i++)
        {
            Vector3 spawnPosition = GetRandomTilePosition();

            GameObject enemy = GameObject.Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            //enemiesInRoomTracker.enemiesInRoom.Add(enemy.GetInstanceID());
            //replace line with add the enemies to killsmanager

            if (isSpawnRoom)
            {
                KillsManager.Instance.TargetsList.Add(enemy.GetComponent<Health>());

            }
        }
        KillsManager.Instance.RefreshRemainingDeaths();
    }

    public Vector3 GetRandomTilePosition()
    {
        // Get the bounds of the primary Tilemap
        BoundsInt bounds = GroundTilemap.cellBounds;

        // Create a list to store all possible tile positions
        List<Vector3Int> validTilePositions = new List<Vector3Int>();

        // Iterate through all tiles in the bounds of the primary Tilemap
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int localPlace = new Vector3Int(x, y, (int)GroundTilemap.transform.position.z);

                // Check if there's a tile in the primary Tilemap and no tile in the exclusion Tilemap
                if (GroundTilemap.HasTile(localPlace) && !ObstaclesTilemap.HasTile(localPlace))
                {
                    validTilePositions.Add(localPlace);
                }
            }
        }

        // Select a random tile position from the list
        if (validTilePositions.Count > 0)
        {
            Vector3Int randomTilePosition = validTilePositions[Random.Range(0, validTilePositions.Count)];
            Vector3 worldPosition = GroundTilemap.CellToWorld(randomTilePosition);
            return worldPosition;
        }

        // If no valid tiles found, return zero vector (or handle accordingly)
        return Vector3.zero;
    }
}
