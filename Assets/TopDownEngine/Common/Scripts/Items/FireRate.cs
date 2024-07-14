using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRate : MonoBehaviour
{
    public GameObject WeaponAttachment;


    public void IncreaseRateOfFire()
    {
        float timeBetweenUses = WeaponAttachment.transform.GetComponentInChildren<ProjectileWeapon>().TimeBetweenUses;

        WeaponAttachment.transform.GetComponentInChildren<ProjectileWeapon>().TimeBetweenUses = timeBetweenUses * .5f;
    }
}
