using Turret;
using UnityEngine;

public class InstantTurret : BaseTurret
{
    public float ExplosiveDamage
    {
        get => (turretType as InstantTurretSO).explosiveDamage;
    }
    public float castTime
    {
        get => (turretType as InstantTurretSO).castTime;
    }
    public float range
    {
        get => (turretType as InstantTurretSO).range;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(range, range, range));
    }
}
