using UnityEngine;

[CreateAssetMenu(fileName = "New Instant Turret", menuName = "Create Turret/Instant Turret")]
public class InstantTurretSO : BaseTurretSO
{
    public int explosiveDamage;
    public int castTime;
    public int range;
}