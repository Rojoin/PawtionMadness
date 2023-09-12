using UnityEngine;

[CreateAssetMenu(fileName = "New Proyectile Turret", menuName = "Create Turret/Proyectile Turret")]
public class ProyectileTurretSO : AttackTurretSO
{
    public Projectile projectile;
    public Transform initialShootPosition;
    public float projectileSpeed;
    public float projectileQuantity;

}