using Turret;
using UnityEngine;

public class TurretFactory
{
    public BaseTurret NewTurretConfigure(BaseTurretSO turret, Transform turretPosition, Transform maxRange, Transform parent)
    {
        BaseTurret newTurret = GameObject.Instantiate(turret.asset, turretPosition.position, Quaternion.identity, parent);

        AttackTurret newAttackTurret = newTurret.GetComponent<AttackTurret>();

        float maxTurretRange = Vector3.Distance(newTurret.transform.position, maxRange.position);

        if (newAttackTurret != null && newAttackTurret.AttackRangeSO > maxTurretRange)
        {
            newAttackTurret.SetAttackRange(maxTurretRange);
        }

        newTurret.Init();
        return newTurret;
    }
}