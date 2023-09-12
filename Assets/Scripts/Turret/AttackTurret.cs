using Health;
using Turret;
using UnityEngine;

public abstract class AttackTurret : BaseTurret
{

    [SerializeField] protected AttackTurretSO turretType;

    protected float shootSpeedTimer = 0;

    public float ShootSpeed { get => turretType.shootSpeed; }
    public int AttackDamage { get => turretType.attackDamage; }
    public int AttackRange { get => turretType.attackRange; }

    public abstract void Shoot();

    public bool DetectEntity()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, AttackRange))
        {
            if (hit.collider.tag == "enemy")
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
