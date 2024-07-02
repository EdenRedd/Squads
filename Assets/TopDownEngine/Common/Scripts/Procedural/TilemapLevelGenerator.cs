using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  MoreMountains.Tools;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace MoreMountains.TopDownEngine
{
	/// <summary>
	/// This component, added on an empty object in your level will handle the generation of a unique and randomized tilemap
	/// </summary>
	public class TilemapLevelGenerator : MMTilemapGenerator
	{
		[FormerlySerializedAs("GenerateOnStart")]
		[Header("TopDown Engine Settings")]
		/// Whether or not this level should be generated automatically on Awake
		[Tooltip("Whether or not this level should be generated automatically on Awake")]
		public bool GenerateOnAwake = false;

		[Header("Bindings")] 
		/// the Grid on which to work
		[Tooltip("the Grid on which to work")]
		public Grid TargetGrid;
		/// the tilemap containing the walls
		[Tooltip("the tilemap containing the walls")]
		public Tilemap ObstaclesTilemap;
        [Tooltip("the tilemap containing the ground")]
        public Tilemap GroundTilemap;
        /// the tilemap containing the walls' shadows
        [Tooltip("the tilemap containing the walls' shadows")]
		public MMTilemapShadow WallsShadowTilemap;
		/// the level manager
		[Tooltip("the level manager")]
		public LevelManager TargetLevelManager;

		[Header("Spawn")] 
		/// the object at which the player will spawn
		[Tooltip("the object at which the player will spawn")]
		public Transform InitialSpawn;
		/// the exit of the level
		[Tooltip("the exit of the level")]
		public Transform Exit;
		/// the minimum distance that should separate spawn and exit.
		[Tooltip("the minimum distance that should separate spawn and exit.")]
		public float MinDistanceFromSpawnToExit = 2f;

		[Header("Enemy Spawns")]
		public int enemyMinAmountToSpawn = 1;

        public int enemyMaxAmountToSpawn = 4;

        public GameObject enemyPrefab;

		public LockTeleporter lockTeleporter;


		protected const int _maxIterationsCount = 100;
        
		/// <summary>
		/// On awake we generate our level if needed
		/// </summary>
		protected virtual void Awake()
		{
			if (GenerateOnAwake)
			{
				Generate();
			}
		}

		/// <summary>
		/// Generates a new level
		/// </summary>
		public override void Generate()
		{
			base.Generate();
			HandleWallsShadow();
            PlaceEntryAndExit();
            if (Application.isPlaying)
            {
                PlaceEnemies(); //Placed before PlaceEntryAndExit because that functions sets a random seed.
            }
            ResizeLevelManager();
            
        }

		protected virtual void PlaceEnemies()
		{
            int enemyAmountToSpawn = UnityEngine.Random.Range(enemyMinAmountToSpawn, enemyMaxAmountToSpawn);
            for (int i = 0; i < enemyAmountToSpawn; i++) {
				Vector3 spawnPosition = GetRandomTilePosition();

                GameObject enemy = GameObject.Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                lockTeleporter.gameObjects.Add(enemy.GetInstanceID());
            }
        }

		/// <summary>
		/// Resizes the level manager's bounds to match the new level
		/// </summary>
		protected virtual void ResizeLevelManager()
		{
			BoxCollider boxCollider = TargetLevelManager.GetComponent<BoxCollider>();
            
			Bounds bounds = ObstaclesTilemap.localBounds;
			boxCollider.center = bounds.center;
			boxCollider.size = new Vector3(bounds.size.x, bounds.size.y, boxCollider.size.z);
        }

		/// <summary>
		/// Moves the spawn and exit to empty places
		/// </summary>
		protected virtual void PlaceEntryAndExit()
		{
			UnityEngine.Random.InitState(GlobalSeed);
			int width = UnityEngine.Random.Range(GridWidth.x, GridWidth.y);
			int height = UnityEngine.Random.Range(GridHeight.x, GridHeight.y);

			Vector3 spawnPosition = MMTilemap.GetRandomPosition(ObstaclesTilemap, TargetGrid, width, height, false, width * height * 2);
			InitialSpawn.transform.position = spawnPosition;

			Vector3 exitPosition = spawnPosition;
			int iterationsCount = 0;
            
			while ((Vector3.Distance(exitPosition, spawnPosition) < MinDistanceFromSpawnToExit) && (iterationsCount < _maxIterationsCount))
			{
				exitPosition = MMTilemap.GetRandomPosition(ObstaclesTilemap, TargetGrid, width, height, false, width * height * 2);
				Exit.transform.position = exitPosition;
				iterationsCount++;
			}
		}
        
		/// <summary>
		/// Copies the contents of the Walls layer to the WallsShadows layer to get nice shadows automatically
		/// </summary>
		protected virtual void HandleWallsShadow()
		{
			if (WallsShadowTilemap != null)
			{
				WallsShadowTilemap.UpdateShadows();
			}
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
}