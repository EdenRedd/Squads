using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIncreaseItem : MonoBehaviour
{
    public void FindPlayerAndGrantAbility()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<HealthIncreasePassive>().IncreaseMaxHealth();
    }
}
