using Turret;
using UnityEngine;

public class InstantTurret : BaseTurret
{
    public int ExplosiveDamage
    {
        get => (turretType as InstantTurretSO).explosiveDamage;
    }
    public int castTime
    {
        get => (turretType as InstantTurretSO).castTime;
    }
    public int range
    {
        get => (turretType as InstantTurretSO).range;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(range, range, range));
    }
}
