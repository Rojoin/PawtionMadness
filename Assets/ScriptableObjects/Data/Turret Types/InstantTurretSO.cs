using UnityEngine;

[CreateAssetMenu(fileName = "New Instant Turret", menuName = "Create Turret/Instant Turret")]
public class InstantTurretSO : BaseTurretSO
{
    public float explosiveDamage;
    public float castTime;
    public float range;
}