using Turret;
using UnityEngine;

[CreateAssetMenu(fileName = "New Turret", menuName = "Turret")]
public class AttackTurretSO : BaseTurretSO
{
    public float shootSpeed;
    public int attackDamage;
    public int attackRange;

}

[CreateAssetMenu(fileName = "New Proyctile Turret", menuName = "Proyctile Turret")]
public class ProyectileTurretSO : AttackTurretSO
{
    public Projectile projectile;
    public Transform initialShootPosition;
    public float projectileSpeed;
    public float projectileQuantity;

}

[CreateAssetMenu(fileName = "New Base Turret", menuName = "Base Turret")]
public class BaseTurretSO : ScriptableObject
{
    public BaseTurret asset;
    public float maxHealth;
}