using Health;
using Turret;
using UnityEngine;

public abstract class AttackTurret : BaseTurret
{
    protected float shootSpeedTimer = 0;

    public float ShootSpeed
    {
        get => (turretType as AttackTurretSO).shootSpeed;
    }
    public int AttackDamage
    {
        get => (turretType as AttackTurretSO).attackDamage;
    }
    public int AttackRange
    {
        get => (turretType as AttackTurretSO).attackRange;
    }

    public abstract void Shoot();

    public bool DetectEntity()
    {
        int layerMask = 1 << gameObject.layer;
        layerMask = ~layerMask;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward , hitInfo: out hit, maxDistance: AttackRange,layerMask))
        {
            Debug.Log(hit.collider.tag);
            if (hit.collider.CompareTag("enemy"))
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, transform.position + (transform.forward * AttackRange));
    }
}