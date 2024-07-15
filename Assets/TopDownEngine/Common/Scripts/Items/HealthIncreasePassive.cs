using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIncreasePassive : MonoBehaviour
{
    public void IncreaseMaxHealth()
    {
        this.gameObject.GetComponent<Health>().MaximumHealth += 10;
        this.gameObject.GetComponent<Health>().CurrentHealth += 10;
    }
}
