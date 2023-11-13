using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Turret", menuName = "Create Turret/Attack Turret")]
public class AttackTurretSO : BaseTurretSO
{
    public float shootSpeed;
    public int attackDamage;
    public float attackRange;
    public float attackDelay = 0.2f;
}