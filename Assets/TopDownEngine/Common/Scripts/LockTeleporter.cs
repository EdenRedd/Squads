using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTeleporter : MonoBehaviour
{

    public void enableTeleporter()
    {
        this.gameObject.GetComponent<Teleporter>().Activable = true;
    }
}
