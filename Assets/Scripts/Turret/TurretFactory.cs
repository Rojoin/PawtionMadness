using Turret;
using UnityEngine;

public class TurretFactory
{
    public void NewTurretConfigure(BaseTurretSO turret, Transform position, Transform maxRange, Transform parent)
    {
        GameObject newTurret = GameObject.Instantiate(turret.asset.gameObject, position.position, position.rotation, parent);

        newTurret.transform.rotation = position.rotation;
        newTurret.transform.position = position.position;
        newTurret.GetComponent<BaseTurret>().Init();
        
        AttackTurret newAttackTurret = newTurret.GetComponent<AttackTurret>();

        float maxTurretRange = Vector3.Distance(newTurret.transform.position, maxRange.position);

        if (newAttackTurret != null && newAttackTurret.AttackRangeSO > maxTurretRange) 
        {
           newAttackTurret.SetAttackRange(maxTurretRange);
        }

    }

}
