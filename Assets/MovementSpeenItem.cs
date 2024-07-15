using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeenItem : MonoBehaviour
{

    public void FindPlayerAndIncreaseMovement()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<MovementSpeenPassive>().IncreaseMovementSpeed();
    }
}
