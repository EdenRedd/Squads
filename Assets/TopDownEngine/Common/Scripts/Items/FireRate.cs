using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRate : MonoBehaviour
{
    public GameObject WeaponAttachment;


    public void IncreaseRateOfFire()
    {
        Debug.Log("CALLED THE FUNCTION ON THE PLAYER");
        float timeBetweenUses = WeaponAttachment.transform.GetComponentInChildren<ProjectileWeapon>().TimeBetweenUses;
        Debug.Log("new time between uses:" + timeBetweenUses * .5f);
        WeaponAttachment.transform.GetComponentInChildren<ProjectileWeapon>().TimeBetweenUses = timeBetweenUses * .5f;
        Debug.Log("we updated time between uses:" + WeaponAttachment.transform.GetComponentInChildren<ProjectileWeapon>().TimeBetweenUses);
    }
}
