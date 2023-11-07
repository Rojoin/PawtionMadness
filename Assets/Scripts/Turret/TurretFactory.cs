using Turret;
using UnityEngine;
using UnityEngine.UIElements;

public class TurretFactory
{
    public void NewTurretConfigure(GameObject newTurret, Transform turretPosition, Transform maxRange)
    {
        newTurret.transform.position = turretPosition.position;

        AttackTurret newAttackTurret = newTurret.GetComponent<AttackTurret>();

        float maxTurretRange = Vector3.Distance(newTurret.transform.position, maxRange.position);

        if (newAttackTurret != null && newAttackTurret.AttackRangeSO > maxTurretRange)
        {
            newAttackTurret.SetAttackRange(maxTurretRange);
        }

        newTurret.GetComponent<BaseTurret>().Init();
    }
}