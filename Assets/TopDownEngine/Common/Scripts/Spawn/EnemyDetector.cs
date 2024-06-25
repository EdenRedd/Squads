using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.TopDownEngine
{
    /// <summary>
	/// A class used to check if there are enemies within the scene that are alive
    /// alive being their health is greater than 0
	/// </summary>
    public class EnemyDetector : MonoBehaviour
    {
        public bool EnemiesInScene()
        {
            GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

            // Iterate through the array and add each enemy to the list
            for (int i = 0; i < enemyObjects.Length; i++)
            {
                if (enemyObjects[i].GetComponent<Health>().CurrentHealth > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
