using UnityEngine;

[CreateAssetMenu(fileName = "New Turret", menuName = "Turret")]
public class AttackTurretType : BaseTurretType
{
    public float shootSpeed;
    public int attackDamage;
    public int attackRange;

}

[CreateAssetMenu(fileName = "New Proyctile Turret", menuName = "Proyctile Turret")]
public class ProyectileTurretType : AttackTurretType
{
    public Projectile projectile;
    public Transform initialShootPosition;
    public float projectileSpeed;
    public float projectileQuantity;

}

[CreateAssetMenu(fileName = "New Base Turret", menuName = "Base Turret")]
public class BaseTurretType : ScriptableObject
{
    public GameObject asset;
    public float maxHealth;
}