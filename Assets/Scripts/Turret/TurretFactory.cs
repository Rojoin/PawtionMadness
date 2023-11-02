using Enemy;
using System.Collections;
using System.Collections.Generic;
using Turret;
using UnityEngine;

public class TurretFactory
{
    public void NewTurretConfigure(ref GameObject turret, Transform position)
    {
        turret.transform.rotation = position.rotation;
        turret.transform.position = position.position;
        turret.GetComponent<BaseTurret>().Init();
    }

}
