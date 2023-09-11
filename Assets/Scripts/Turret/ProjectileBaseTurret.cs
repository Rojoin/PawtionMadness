using UnityEngine;

public class ProjectileBaseTurret : AttackTurret
{
    [SerializeField] ProyectileTurretType ProyectileTurretType;

    public Projectile Projectile { get => ProyectileTurretType.projectile;}
    public Transform InitialShootPosition { get => ProyectileTurretType.initialShootPosition; }
    public float ProjectileSpeed { get => ProyectileTurretType.projectileSpeed; }
    public float ProjectileQuantity { get => ProyectileTurretType.projectileQuantity; }

    public void Update()
    {
        shootSpeedTimer += Time.deltaTime;

        if (shootSpeedTimer > ShootSpeed)
        {
            shootSpeedTimer = 0;
            if (DetectEntity())
            {
                Shoot();
            }
        }
    }
    public override void Shoot()
    {
        for (int i = 0; i < ProjectileQuantity; i++)
        {
            Projectile newProjectile = Instantiate(Projectile, InitialShootPosition.position, InitialShootPosition.rotation);
            newProjectile.ProjectileSpeed = ProjectileSpeed;
            newProjectile.Damage = AttackDamage;
        }
    }
}
