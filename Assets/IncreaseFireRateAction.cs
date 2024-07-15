using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseFireRateAction : MonoBehaviour
{
    public void IncreasePlayerFireRate()
    {
        Debug.Log("CALLED THE INCREASE FIRE RATE FUNCTION");
        GameObject.FindGameObjectWithTag("Player").GetComponent<FireRate>().IncreaseRateOfFire();
    }
}
