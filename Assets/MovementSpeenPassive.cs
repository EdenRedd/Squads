using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementSpeenPassive : MonoBehaviour
{
    public void IncreaseMovementSpeed()
    {
        this.gameObject.GetComponent<CharacterMovement>().WalkSpeed += 3;
    }
}
